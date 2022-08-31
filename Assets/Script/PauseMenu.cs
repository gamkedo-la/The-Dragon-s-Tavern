using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pausePanel, audioPanel, buttonPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        buttonPanel.SetActive(true);
        audioPanel.SetActive(false);
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }

    public void ToggleAudioPanel(){
        audioPanel.SetActive(!audioPanel.activeInHierarchy);
        buttonPanel.SetActive(!buttonPanel.activeInHierarchy);
    }
}
