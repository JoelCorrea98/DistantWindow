using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    void Update ()
    { 
        if (Input.GetKeyDown(KeyCode.R))
	    {
            Level1Scene();
	    }    
    }

    public void IntroScene()
    { SceneManager.LoadScene("IntroductionScene"); }

    public void WinScene()
    { SceneManager.LoadScene("WinScene"); }

    public void LoseScene()
    { SceneManager.LoadScene("WinScene"); }

    public void MainMenuScene()
    { SceneManager.LoadScene("MainScene"); }

    public void Level1SceneOLD()
    { SceneManager.LoadScene("DistantWindowLVL1"); }

    public void Level2Scene()
    { SceneManager.LoadScene("DistantWindowLVL2"); }

    public void CreditsScene()
    { SceneManager.LoadScene("CreditsScene"); }

    public void Level1Scene()
    { SceneManager.LoadScene("DW-LVL1"); }

    public void QuitScene()
    { Application.Quit(); }
}
