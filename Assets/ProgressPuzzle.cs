using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPuzzle : MonoBehaviour
{

    private readonly int progress_threshold = 5000;
    private int progress = 0;

    private int lever1_counter = 1;
    private int lever2_counter = 1;
    private int lever3_counter = 1;
    private int lever4_counter = 1;

    public TextMesh mesh;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mesh.text = progress.ToString();


    }


    public void lever1()
    {
        progress += 100;
        lever1_counter++;

        if (lever1_counter % 3 == 0)
        {
            progress -= 50;
        }

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }


    }

    public void lever2()
    {
        progress += 250;
        lever2_counter++;

        if (lever2_counter % 7 == 0)
        {
            progress -= 700;
        }

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }
    }

    public void lever3()
    {
        progress += 1000;
        lever3_counter++;

        if (lever3_counter % 4 == 0)
        {
            progress = 0;
        }

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }
    }

    public void lever4()
    {
        progress += 1000;
        lever4_counter++;

        if (lever4_counter % 4 == 0)
        {
            progress = 0;
        }

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }
    }
}
