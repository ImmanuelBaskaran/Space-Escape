using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Leap.Unity;

public class TeleportTablet : MonoBehaviour
{

    Transform player;
    Transform camera;

    public Transform[] targets;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = Camera.main.transform;
    }

    public void TeleportTo(int i)
    {


        //Valve.VR.OpenVR.Compositor.SetTrackingSpace(Valve.VR.ETrackingUniverseOrigin.TrackingUniverseSeated);
        //Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        player.parent = targets[i];
        player.localPosition = Vector3.zero;
        Vector3 offset = camera.position - player.position;
        player.position -= offset;
        player.Translate(0, player.GetComponent<XRHeightOffset>().roomScaleHeightOffset, 0, Space.World);
    }
}
