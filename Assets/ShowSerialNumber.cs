using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using Valve.VR;

public class ShowSerialNumber : MonoBehaviour
{
    void Update()
    {
        var error = ETrackedPropertyError.TrackedProp_Success;
        var i = GetComponent<SteamVR_TrackedObject>().index;
        var result = new System.Text.StringBuilder((int)64);

        OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_SerialNumber_String, result, 64, ref error);
    //    Debug.Log(result.ToString());

        OpenVR.System.GetStringTrackedDeviceProperty((uint)i, ETrackedDeviceProperty.Prop_ConnectedWirelessDongle_String, result, 64, ref error);
   //     Debug.Log(result.ToString());
    }
}
