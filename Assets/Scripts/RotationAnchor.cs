using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnchor : MonoBehaviour
{
    public GameObject button;
    public GameObject jigsaw;
    [HideInInspector]public bool movingObject = false;
    private float buttonRadius = 2f;

    // Start is called before the first frame update
    void Start()
    {
        buttonRadius = Vector3.Distance(transform.position, button.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        //Gets the world position of the mouse on the screen        
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //Checks whether the mouse is over the sprite
        bool overSprite = button.GetComponent<SpriteRenderer>().bounds.Contains(mousePosition);

        //If it's over the sprite
        if (overSprite)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                movingObject = true;
            }

        }
        if (Input.GetButton("Fire1") && movingObject)
        {
            Vector3 worldMousePos = mousePosition;
            var direction = worldMousePos - transform.position;
            direction.Normalize();


            //Set the position to the mouse position
            button.transform.localPosition = new Vector3(direction.x * buttonRadius,
                direction.y * buttonRadius,
                0.0f);

            float angle = Mathf.Atan2(button.transform.localPosition.y - transform.localPosition.y, button.transform.localPosition.x - transform.localPosition.x) * 180 / Mathf.PI;
            
            jigsaw.transform.rotation =  Quaternion.Euler(0.0f, 0.0f, angle);
            transform.rotation = Quaternion.Euler(0.0f, 0.0f, -jigsaw.transform.rotation.z);
            button.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 45f);


        }
        if (Input.GetButtonUp("Fire1") && movingObject)
        {
            movingObject = false;
        }
    }
}
