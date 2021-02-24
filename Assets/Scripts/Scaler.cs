using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    float maxWidth = 0.9367f;
    public bool debugData = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float width = ScreenSize.GetScreenToWorldWidth/GameManager.Instance.scalingRatio;  
        float fixedWidth = Mathf.Clamp(width, 0f, maxWidth);

        transform.localScale = Vector3.one * fixedWidth;

        float height = ScreenSize.GetScreenToWorldHeight/GameManager.Instance.scalingRatio;  
        /* transform.localScale = Vector3.one * height; */
        if(debugData){
            Debug.Log("height-> "+ height);
            Debug.Log("width-> "+ width);
        }
    }
}
