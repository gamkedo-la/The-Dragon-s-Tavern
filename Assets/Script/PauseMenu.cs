using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

    [SerializeField] GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePausePanel();
        }
    }

    public void TogglePausePanel()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
    }
}
