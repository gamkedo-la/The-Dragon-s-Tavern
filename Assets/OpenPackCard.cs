using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenPackCard : MonoBehaviour
{
    public GameObject cardToReceive;
    // Start is called before the first frame update

    public void TurnCardOn()
    {
        cardToReceive.SetActive(true);
    }
}
