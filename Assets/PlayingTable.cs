using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayingTable : MonoBehaviour
{
    public Image[] playableSpots;

    private void Update()
    {
        if (GameManager.hasBeenPulled)
        {
            for (int i = 0; i < playableSpots.Length; i++)
            {
              //  playableSpots[i].GetComponent<Image>().color = Color.blue;
            }
        }
        else
        {
            for (int i = 0; i < playableSpots.Length; i++)
            {
              //  playableSpots[i].GetComponent<Image>().color = Color.white;
            }
        }
    }
}
