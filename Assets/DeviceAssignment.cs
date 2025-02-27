﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Valve.VR;

/// <summary>
/// Sets the attached SteamVR_TrackedObject script to track a Device that has a Serial Number in the list
/// </summary>
public class DeviceAssignment : MonoBehaviour
{
    public string[] serialNumbers;

    private void OnEnable()
    {

        SteamVR_Events.DeviceConnected.Listen(new UnityEngine.Events.UnityAction<int, bool>(OnDeviceConnected));
    }

    // This will be called every time a new device is connected or detected on start up.
    private void OnDeviceConnected(int i, bool connected)
    {
        
        // This section checks the serial number of the new device
        var error = ETrackedPropertyError.TrackedProp_Success;
        var result = new System.Text.StringBuilder((int)64);
        OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, result, 64, ref error);
        Debug.Log(result);
        // This section checks if this new device fulfills the role of the current GameObject...
        if (serialNumbers.Contains(result.ToString()))
        {
            // ...and if so sets the current SteamVR_TrackedObject component to track it
            GetComponent<SteamVR_TrackedObject>().SetDeviceIndex(i);
        }
    }
}
