using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMovement : MonoBehaviour
{
    private bool puzzleSolved = false;
    private bool currentSelectedObj = false;
    [HideInInspector]public bool isInRotation = false;
    [SerializeField] public GameObject outline;
    [SerializeField] public GameObject rotateBtn;
    private Animator animator;
    public AudioSource selectSound;
    public AudioSource solveSound;


    private void Start() {
        animator = GetComponent<Animator>();

    }
    public void solvePuzzle(){
        solveSound.Play();
        Debug.Log("solve puzzle inside obj being called");
        this.outline.SetActive(false);
        this.rotateBtn.SetActive(false);
        puzzleSolved = true;
    }
    void CheckIfActive(){
        if(GameManager.Instance.getPreviousSelectedObj() == gameObject.GetComponent<ObjMovement>()){
            this.outline.SetActive(true);
            this.rotateBtn.SetActive(true);
        }else {
            this.outline.SetActive(false);
            this.rotateBtn.SetActive(false);
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, -0.01f);

        }
    }
    void Update()
    {
        isInRotation = rotateBtn.GetComponent<RotationAnchor>().movingObject;
        animator.SetBool("Holding", currentSelectedObj);
        animator.SetBool("Solved", puzzleSolved);


        if(puzzleSolved){
            return;
        }
        CheckIfActive();

        if (Input.GetButtonDown("Fire1")){
            selectSound.Play();
            //Gets the world position of the mouse on the screen        
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint( Input.mousePosition );

            //Checks whether the mouse is over the sprite
            bool overSprite = this.GetComponent<SpriteRenderer>().bounds.Contains( mousePosition );
            
            if(!overSprite){
                return;
            }

            if(GameManager.Instance.getCurrentSelectedObj() == null){
                GameManager.Instance.SetCurrentSelectObj(gameObject.GetComponent<ObjMovement>());
                currentSelectedObj = true;
            }
            if(GameManager.Instance.getCurrentSelectedObj() != gameObject.GetComponent<ObjMovement>()){
                currentSelectedObj = false;
                return;
            }
        }

        //If we've pressed down on the mouse (or touched on the iphone)
        if (Input.GetButton("Fire1") && currentSelectedObj)
        {
            //Set the position to the mouse position
            this.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,
                Camera.main.ScreenToWorldPoint(Input.mousePosition).y,
                -0.03f);
        }
        if (Input.GetButtonUp("Fire1") && currentSelectedObj){
            GameManager.Instance.UnsetCurrentSelectObj();
            currentSelectedObj = false;

        }
        

        /* if (Input.touchCount > 0) 
        {
            Touch touch = Input.GetTouch(0);
            
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) 
            {
                Vector3 touchedPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 10));
                transform.position = Vector3.Lerp(transform.position, touchedPos, .1f);
            }
        } */
    }
}
