using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPuzzle : MonoBehaviour
{

    public int START_VALUE = 111;
    public int TARGET_VALUE = 5000;

    private readonly int progress_threshold = 5000;
    private long progress;
    private bool done = false;

    private int lever1_counter = 1;
    private int lever2_counter = 1;
    private int lever3_counter = 1;
    private int lever4_counter = 1;

    public TextMesh mesh;

    // Start is called before the first frame update
    void Start()
    {
        progress = START_VALUE;
    }

    // Update is called once per frame
    void Update()
    {
        if (!done)
        {
            mesh.text = progress.ToString();
        } else
        {
            mesh.text = "Stabilised";
            PuzzleState.Solved(1);
        }


    }


    public void lever1()
    {
        

        if (progress % 2 == 0)
        {
            progress /=2;
        }
        else
        {
            progress *= 3;
            progress++;

        }

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }


    }

    public void lever2()
    {

        int newProgress = 0;

        foreach(char c in progress.ToString())
        {
            newProgress += int.Parse(c.ToString());
        }
        progress = newProgress;

        if (progress > progress_threshold)
        {
            // Unlock the portal
        }
    }

    public void lever3()
    {

        progress = progress * progress;

        if (progress < 0)
        {
            progress = START_VALUE;
        }
    }

    public void lever4()
    {
        
        if(progress == TARGET_VALUE)
        {
            done = true;
        }
        else
        {
            progress = START_VALUE;
        }
    }
}
