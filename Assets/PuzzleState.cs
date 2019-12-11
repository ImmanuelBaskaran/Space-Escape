using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PuzzleState : MonoBehaviour
{
    public bool[] puzzlesSolved;

    public GameObject[] solvedMarkers;

    static bool debounceAudio;
    public GameObject allSolvedMarker;

    public AudioClip allClearClip;

    public static AudioClip freedom;
    public static AudioClip puzzleSolve;
    public static AudioSource speaker;

    public void Start()
    {
        speaker = GetComponent<AudioSource>();
        freedom = allClearClip;
        debounceAudio = false;
    }

    public static void Solved(int i)
    {
        PuzzleState state = GameObject.FindGameObjectWithTag("PuzzleState").GetComponent<PuzzleState>();
        state.puzzlesSolved[i] = true;
        state.solvedMarkers[i].SetActive(true);

        if(state.puzzlesSolved.All(x => x) && !debounceAudio)
        {
            debounceAudio = true;
            state.allSolvedMarker.SetActive(true);
            Debug.Log("Testing");
            speaker.clip = freedom;
            speaker.PlayDelayed(3);
        } else
        {
            speaker.PlayOneShot(puzzleSolve);
        }
    }
    public void Solve(int i)
    {
        Solved(i);
    }
}
