using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timeAlive = .75f;

    void Update()
    {
        timeAlive -= Time.deltaTime;
        if (timeAlive <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
