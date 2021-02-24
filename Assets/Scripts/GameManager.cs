using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static GameManager _instance;
    private ObjMovement currentSelectedObj = null;
    private ObjMovement previousSelectedObj = null;

    public static GameManager Instance { get { return _instance; } }
    public float scalingRatio = 21.7f;
    public bool showPuzzleSolution = false;

    private Puzzle[] puzzles;


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start() {
        puzzles = FindObjectsOfType<Puzzle>();
        foreach (Puzzle puzzle in puzzles)
        {
            if(!showPuzzleSolution){
                puzzle.HideSolution();
            }
        }

    }

    public void CheckGameStatus(){
        bool gameFinished = true;
        foreach (Puzzle puzzle in puzzles)
        {
            if(!puzzle.puzzleSolved){
                gameFinished = false;
            }
        }
        if(gameFinished){
            GameObject.FindWithTag("Canvas").GetComponent<UiController>().ShowPanel();
        }

    }


    public void SetCurrentSelectObj(ObjMovement value){
        this.currentSelectedObj = value;
        this.previousSelectedObj = value;
    }
    public void UnsetCurrentSelectObj(){
        this.currentSelectedObj = null;
    }
    public ObjMovement getCurrentSelectedObj(){
        return this.currentSelectedObj;
    }
    public ObjMovement getPreviousSelectedObj(){
        return this.previousSelectedObj;
    }
}

