using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SceneToAR()
    {
        ARPlaceOnPlane.isChoose = false;
        SceneManager.LoadScene("ARScene");
    }
    public void SceneToGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void SceneToStart()
    {
        SceneManager.LoadScene("StartScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
