using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingTable : MonoBehaviour
{
    public Image[] playableSpots;
    bool isHovering;

    private void Update()
    {
        if (GameManager.hasBeenPulled)
        {
            for (int i = 0; i < playableSpots.Length; i++)
            {
                playableSpots[i].GetComponent<Image>().color = Color.blue;
            }

            if (isHovering)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Debug.Log("Hovering fine");
                    //Instantiate Card here
                    //Destroy Card in hand
                    //Update costs
                }
            }
        }
        else
        {
            for (int i = 0; i < playableSpots.Length; i++)
            {
                playableSpots[i].GetComponent<Image>().color = Color.white;
            }
        }
    }

    public void CardZoneEnter()
    {
        isHovering = true;
    }
    public void CardZoneExit()
    {
        isHovering = false;
    }
}
