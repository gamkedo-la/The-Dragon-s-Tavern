using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject tut, cred, main;

    public void Tutorial() // exit the game from the main menu
    {
        tut.SetActive(true);
        main.SetActive(false);
    }
    public void StartGame() // start the game by pressing the play button on the main menu
    {
        SceneManager.LoadScene("HubArea");
    }

    public void Credits() // start the game by pressing the play button on the main menu
    {
        cred.SetActive(true);
        main.SetActive(false);
    }
}
