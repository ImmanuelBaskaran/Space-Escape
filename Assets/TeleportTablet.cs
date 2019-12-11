using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Leap.Unity;
[System.Serializable]
public class ClipArray
{
    public AudioClip[] clips;

    public int Length
    {
        get
        {
            return clips.Length;
        }
    }

    public AudioClip this[int index]
    {
        get
        {
            return clips[index];
        }
        
        set
        {
            clips[index] = value;
        }
    }
}

[RequireComponent(typeof(AudioSource))]
public class TeleportTablet : MonoBehaviour
{

    Transform player;
    Transform camera;

    public Transform[] targets;
    public ClipArray[] clips;
    public AudioSource speaker;
    bool[] visited;
    int currentRoom;
    int currentClip;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        camera = Camera.main.transform;
        currentRoom = 1;
        currentClip = 0;
        speaker = GetComponent<AudioSource>();
        visited = new bool[targets.Length];
        for (int i = 0; i < visited.Length; i++)
        {
            visited[i] = false;
        }
    }

    public void Help ()
    {
        speaker.PlayOneShot(clips[currentRoom][currentClip]);
        currentClip++;

        currentClip = currentClip % clips[currentRoom].Length;
        
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

        currentRoom = i;
        currentClip = 0;

        if (!visited[currentRoom])
        {
            speaker.PlayOneShot(clips[currentRoom][currentClip]);
            visited[i] = true;
            currentClip++;
        }

        
    }
}
