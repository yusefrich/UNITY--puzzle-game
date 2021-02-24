using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject panel;
    // Start is called before the first frame update
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene
    }
    public void PlaySecondPuzzle()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name+"2"); // loads second scene
    }
    public void ShowPanel()
    {
        panel.SetActive(true);
    }

}
