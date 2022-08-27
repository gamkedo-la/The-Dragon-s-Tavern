using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SpellTrail : MonoBehaviour
{

    Vector3 target;


    public void SetTarget(Vector3 _target){
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            return;
        }

        if(Vector3.Distance(target, transform.position) < 0.01f){
            Destroy(gameObject);
        }else{
            transform.position = Vector3.MoveTowards(transform.position, target, 0.02f);
        }
    }
}
