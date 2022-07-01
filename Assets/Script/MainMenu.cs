using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void ExitButton() // exit the game from the main menu
    {
        Application.Quit();
        Debug.Log("Exiting the game");
    }
    public void StartGame() // start the game by pressing the play button on the main menu
    {
        SceneManager.LoadScene("HubArea");
    }
}
