using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    public Sprite[] options;

    void Start() {
        SpriteRenderer mySR = GetComponent<SpriteRenderer>();
        mySR.sprite = options[Random.Range(0,options.Length)];
    }
}
