using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WireComponent : MonoBehaviour
{

    public delegate void Callback(int x, int y);

    public Callback callback;
    private int x, y;

    public void setPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    // Start is called before the first frame update
    void Start()
    {
               
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider a)
    {
        callback(x, y);
        //Debug.Log("You hit me!");
        transform.Rotate(0,0,-90);
    }
}
