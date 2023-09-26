using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenemanager : MonoBehaviour
{
    public void IntroScene()
    { SceneManager.LoadScene("IntroductionScene"); }

    public void WinScene()
    { SceneManager.LoadScene("WinScene"); }

    public void LoseScene()
    { SceneManager.LoadScene("WinScene"); }

    public void MainMenuScene()
    { SceneManager.LoadScene("MainScene"); }

    public void Level1Scene()
    { SceneManager.LoadScene("DistantWindowLVL1"); }

    public void Level2Scene()
    { SceneManager.LoadScene("DistantWindowLVL2"); }

    public void CreditsScene()
    { SceneManager.LoadScene("CreditsScene"); }

    public void QuitScene()
    { Application.Quit(); }
}
