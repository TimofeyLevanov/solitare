using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void playRandomGame()
    {
        SceneManager.LoadScene("RandomGame");
    }
    public void playWinningGame()
    {

    }
    public void exit()
    {
        Application.Quit();
    }
}
