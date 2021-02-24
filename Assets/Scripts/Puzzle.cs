using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle : MonoBehaviour
{
    [SerializeField] public bool puzzleSolved;
    [SerializeField] public Transform jigSaw;
    [SerializeField] public Transform placement;
    [SerializeField] public float minDistance = 3f;

    // Start is called before the first frame update
    void Start()
    {
        puzzleSolved = false;
        bool invalidPosition = true;

        while (invalidPosition)
        {
            this.RandomizeJigsawTransform();

            if(!CheckPlacement()){
                invalidPosition=false;
            }
        }
    }
    public void HideSolution(){
        placement.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0f);
    }
    void RandomizeJigsawTransform(){
        float width = ScreenSize.GetScreenToWorldWidth/2;  
        float height = ScreenSize.GetScreenToWorldHeight/2;  

        jigSaw.position = new Vector3(Random.Range(-width, width), Random.Range(-height, height), 0f);
        float rotationValue = Random.Range(0f, 360f);
        jigSaw.rotation = Quaternion.Euler(0.0f, 0.0f, rotationValue);
        jigSaw.GetComponent<ObjMovement>().rotateBtn.transform.rotation =  Quaternion.Euler(0.0f, 0.0f, jigSaw.rotation.z * -1f);

    }

    // Update is called once per frame
    void Update()
    {
        if(puzzleSolved){
            return;
        }

        if(CheckPlacement()){
            SolvePuzzle();
            GameManager.Instance.CheckGameStatus();
        }

    }

    bool CheckPlacement(){
        float dist = Vector3.Distance(placement.position, jigSaw.position);
        bool currentJigSaw = GameManager.Instance.getCurrentSelectedObj() ==  jigSaw.GetComponent<ObjMovement>();
        bool currentRotatingJigSaw = jigSaw.GetComponent<ObjMovement>().isInRotation;
        bool onPosition = dist <= minDistance && !currentJigSaw;
        bool onRotation = (jigSaw.localEulerAngles.z < 10 || jigSaw.localEulerAngles.z >350) && !currentRotatingJigSaw;
        return onPosition && onRotation;
    }

    public void SolvePuzzle(){
        puzzleSolved = true;
        jigSaw.position = placement.position;
        jigSaw.rotation = Quaternion.Euler(0.0f, 0.0f, 0f);
        jigSaw.GetComponent<ObjMovement>().solvePuzzle();
    }
}
