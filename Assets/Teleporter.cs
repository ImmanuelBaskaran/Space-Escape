using Leap.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class Teleporter : MonoBehaviour
{
    public Transform target;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag.Equals("PlayerHead"))
        {
            //Transform leapRig = GameObject.FindGameObjectWithTag("Player").transform;
            //leapRig.parent = target;
            //leapRig.localPosition = Vector3.zero;
            //XRSupportUtil.Recenter();


        }
    }
}
