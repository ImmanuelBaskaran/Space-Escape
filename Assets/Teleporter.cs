using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]

public class Teleporter : MonoBehaviour
{
    public GameObject from;
    public GameObject to;

    void OnTriggerEnter()
    {
        from.SetActive(false);
        to.SetActive(true);
    }
}
