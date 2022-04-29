using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPlacement : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 30, 0);
    public void CardHoverEnter()
    {
        this.gameObject.transform.localPosition += offset;
    }

    public void CardHoverExit()
    {
        this.gameObject.transform.localPosition -= offset;
    }
}
