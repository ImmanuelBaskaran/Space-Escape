using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleState : MonoBehaviour
{
    public bool[] puzzlesSolved;

    public GameObject[] solvedMarkers;


    public GameObject allSolvedMarker;
 

    public static void Solved(int i)
    {
        PuzzleState state = GameObject.FindGameObjectWithTag("PuzzleState").GetComponent<PuzzleState>();
        state.puzzlesSolved[i] = true;
        state.solvedMarkers[i].SetActive(true);

        if(state.puzzlesSolved.All(x => x))
        {
            state.allSolvedMarker.SetActive(true);
        }
    }
}
