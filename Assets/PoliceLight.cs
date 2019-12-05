using UnityEngine;
using System.Collections;

public class PoliceLight : MonoBehaviour
{

    public Light RedLight;
    public Light BlueLight;
    public Light YellowLight;
    public Light GreenLight;
    public int Number = 1;
    private int count =0;
    private int a = 0;
    // Use this for initialization
    void Start()
    {
        Number = 1;
        BlueLight.GetComponent<Light>().intensity = 0;
        RedLight.GetComponent<Light>().intensity = 0;
        YellowLight.GetComponent<Light>().intensity = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Number == 1)
        {
            BlueLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 0;
            GreenLight.GetComponent<Light>().intensity = 0;
            RedLight.GetComponent<Light>().intensity = 1.5f;
            StartCoroutine(waitforred());
        }
        if (Number == 2)
        {
            RedLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 0;
            GreenLight.GetComponent<Light>().intensity = 0;
            BlueLight.GetComponent<Light>().intensity = 1.5f;
            StartCoroutine(waitforblue());
        }
        if (Number == 3)
        {
            BlueLight.GetComponent<Light>().intensity = 0;
            RedLight.GetComponent<Light>().intensity = 0;
            GreenLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 1.5f;
            StartCoroutine(waitforyellow());
        }
        if (Number == 4)
        {
            BlueLight.GetComponent<Light>().intensity = 0;
            RedLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 0;
            GreenLight.GetComponent<Light>().intensity = 1.5f;
            StartCoroutine(waitforgreen());

        }
        if (Number == 5)
        {
            BlueLight.GetComponent<Light>().intensity = 1.5f;
            RedLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 0;
            GreenLight.GetComponent<Light>().intensity = 0;
            StartCoroutine(waitforblue2());

        }
        if (Number == 6)
        {
            BlueLight.GetComponent<Light>().intensity = 0;
            RedLight.GetComponent<Light>().intensity = 0;
            YellowLight.GetComponent<Light>().intensity = 1.5f;
            GreenLight.GetComponent<Light>().intensity = 0;
            StartCoroutine(waitforyellow2());

        }
    }
    IEnumerator waitforred()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 2;
    }
    IEnumerator waitforblue()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 3;
    }
    IEnumerator waitforyellow()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 4;
    }
    IEnumerator waitforgreen()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 5;
    }
    IEnumerator waitforblue2()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 6;
    }
    IEnumerator waitforyellow2()
    {
        yield return new WaitForSeconds(0.7f);
        Number = 1;
    }

}