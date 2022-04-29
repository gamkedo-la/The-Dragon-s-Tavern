using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 30, 0);
    public Vector3 pullFromHand = new Vector3(0, -175f, 0);

    bool hasEntered;
    bool hasBeenPulled;
    public void CardHoverEnter()
    {
        if (!hasBeenPulled)
        {
            this.gameObject.transform.localPosition += offset;
            hasEntered = true;
        }
    }

    public void CardHoverExit()
    {
        if (!hasBeenPulled)
        {
            this.gameObject.transform.localPosition -= offset;
            hasEntered = false;
        }
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasEntered && !hasBeenPulled)
        {
            this.gameObject.transform.localPosition -= pullFromHand;
            GameObject.Find("PlayerHand").transform.localPosition += pullFromHand;
            hasBeenPulled = true;
            GameManager.hasBeenPulled = true;
        }

        if (Input.GetMouseButtonDown(1) && hasEntered && hasBeenPulled)
        {
            this.gameObject.transform.localPosition += pullFromHand;
            GameObject.Find("PlayerHand").transform.localPosition -= pullFromHand;
            hasBeenPulled = false;
            GameManager.hasBeenPulled = false;
        }
    }
}
