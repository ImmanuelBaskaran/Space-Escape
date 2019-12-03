using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Leap.Unity.Interaction;


[RequireComponent(typeof(MeshRenderer))]
public class WiringPuzzle : MonoBehaviour
{

    public int gridSizeX;
    public int gridSizeY;

    public Vector2Int[] sources;
    public Vector2Int[] sinks;

    int[,] gridState;

    public Material[] cases;
    public Color[] sourceColors;

    private int[,] puzzlepieces = {{0,1,1,0,0},
                                   {1,1,1,2,4},
                                   {4,4,2,4,4},
                                   {2,4,1,1,4},
                                   {1,4,2,1,0}};


    private int[,] orientations = { {1,2,1,2,3},
                                    {3,0,3,1,0},
                                    {1,3,2,1,0},
                                    {1,3,2,1,0},
                                    {1,3,2,1,3}};


    public GameObject case1;
    public GameObject case2;
    public GameObject case3;
    public GameObject case4;
    public GameObject case5;


    private bool canMoveTo(int tileCase, int orientation, int direction) {
        Debug.Log("I am a " + tileCase + " coming from "+direction + " to the piece that is facing "+orientation);
        if (tileCase == 0) {
            return (orientation == direction);
        }
        if (tileCase == 4)
        {
            return orientation == (direction) || (orientation + 2) % 4 == direction;
        }
        else if (tileCase == 2)
        {
            return orientation == (direction) || (orientation + 2) % 4 == direction || (orientation + 1) % 4 == direction;
        }

        else if (tileCase == 1)
        {
            return orientation == (direction) || (orientation + 1) % 4 == direction;
        }
        else if (tileCase == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    private int[] getNeighbors(int tileCase, int orientation)
    {
        int[] orientations;
        //# souce/sink
        if (tileCase == 0)
        {
            return new int[] { orientation };
        }
        //   #line
        else if (tileCase == 4)
        {
            return new int[] { orientation, (orientation + 2) % 4 };
        }
        else if (tileCase == 2)
        {
            return new int[] { orientation, (orientation + 2) % 4, (orientation + 1) % 4 };
        }
        else if (tileCase == 1)
        {
            return new int[] { orientation, (orientation + 1) % 4 };
        }
        //   # Cross
        else if (tileCase == 3)
        {
            return new int[] { 0, 1, 2, 3 };
        }
        else
        {
            return null;
        }
    }

    public int[] mapToDirection(int outDir) {
        int incomingDirection = (2 + outDir) % 4;
        if (outDir == 0) {
            return new int[] { 0, -1, (incomingDirection) };
        }
        else if (outDir == 1) {
            return new int[] { 1, 0, incomingDirection };
        }
        else if (outDir == 2) {
            return new int[] { 0, 1, (incomingDirection) };
        }
        else if(outDir == 3){
            return new int[] { -1, 0, incomingDirection };
        }
        else{
            return null;
        }
    }


    private bool pathTrace(int[,] cases, int[,] orientation,int x, int y)
    {
        int count = 0;
        int x1 = x;
        int y1 = y;
        int rowsOrHeight = cases.GetLength(0);
        int colsOrWidth = cases.GetLength(1);
        bool[] seen = new bool[rowsOrHeight * colsOrWidth];
        List<int[]> filteredNeighbors = new List<int[]>();
        bool found = false;
        while (true) {
            Debug.Log(x1 + " " + y1);
            seen[x1 + y1 * rowsOrHeight] = true;
            int[][] neighbors = getNeighbors(cases[y1, x1], orientation[y1, x1])
                .Select(mapToDirection)
                .Select(v => { Debug.Log((v[0] + x1) + "," + (v[1]+ y1));  return new int[] { v[0] + x1, v[1] + y1, v[2] }; })
                .ToArray();


            foreach (int[] n in neighbors) {
                if (n[1] >= rowsOrHeight || n[0] >= colsOrWidth || n[1] < 0 || n[0] < 0) continue;
                
                if (!seen[n[1] * rowsOrHeight + n[0]]) {
                    Debug.Log("I am at "+x1+":"+y1);
                    if (canMoveTo(cases[n[1], n[0]], orientation[n[1], n[0]], n[2]))
                    {
                        filteredNeighbors.Add(n);
                    }
                   
                }
            }


            if (filteredNeighbors.Count == 0)
                    break;

            x1 = filteredNeighbors[0][0];
            y1 = filteredNeighbors[0][1];
            if (cases[y1, x1] == 0) {
                found = true;
                break;
            }

            filteredNeighbors.RemoveAt(0);
        }
        return found;
    }
    


    Vector3 gridIndexToLocalCoord(int x, int y)
    {
        Vector3 extents = transform.GetChild(0).GetComponent<MeshRenderer>().bounds.extents;
        return 2 * (Mathf.Abs(Vector3.Dot(extents, transform.right)) * extents * (((float)x) / gridSizeX)
            + extents.y * transform.up * (((float)y) / gridSizeY));
    }

    Vector3 gridIndexToWorldCoord(int x, int y)
    {
        return gridIndexToLocalCoord(x, y) + transform.position;
    }
    // Start is called before the first frame update
    void Start()
    {



        // Instantiate at position (0, 0, 0) and zero rotation.
        for (int i = 0; i >= -4; i--)
        {
            for (int j = 0; j >= -4; j--)
            {
                Vector3 test = this.transform.position;
                test.y += -0.055f + (-i * 0.125f);
                test.z += -0.055f + (-j * 0.125f);
                int theCase = puzzlepieces[i + 4 , j + 4];
                int orientation = orientations[i + 4, j + 4];
                if (theCase == 0) {
                    GameObject tile =  Instantiate(case1, test, Quaternion.Euler(0, 90, orientation * -90), transform);
                    WireComponent component = tile.GetComponent(typeof(WireComponent)) as WireComponent;
                    component.callback = invalidate;
                    component.setPosition(i + 4, j + 4);
                }
                if (theCase == 1)
                {
                    GameObject tile = Instantiate(case2, test, Quaternion.Euler(0, 90, orientation * -90), transform);
                    WireComponent component = tile.GetComponent(typeof(WireComponent)) as WireComponent;
                    component.callback = invalidate;
                    component.setPosition(i + 4, j + 4);
                }
                if (theCase == 2)
                {
                    GameObject tile = Instantiate(case3, test, Quaternion.Euler(0, 90, orientation * -90), transform);
                    WireComponent component = tile.GetComponent(typeof(WireComponent)) as WireComponent;
                    component.callback = invalidate;
                    component.setPosition(i + 4, j + 4);
                }
                if (theCase == 3)
                {
                    GameObject tile = Instantiate(case4, test, Quaternion.Euler(0, 90, orientation * 90), transform);
                    WireComponent component = tile.GetComponent(typeof(WireComponent)) as WireComponent;
                    component.callback = invalidate;
                    component.setPosition(i + 4, j + 4);
                }
                if (theCase == 4)
                {
                    GameObject tile = Instantiate(case5, test, Quaternion.Euler(0, 90, orientation * -90), transform);
                    WireComponent component = tile.GetComponent(typeof(WireComponent)) as WireComponent;
                    component.callback = invalidate;
                    component.setPosition(i+4, j + 4);
                }
            }
        }
        //Debug.Log(pathTrace(puzzlepieces, orientations,0,0));
    }

    // Update is called once per frame
    void Update()
    {
   
    }

    void OnDrawGizmos()
    {
        //     Gizmos.DrawWireSphere(gridIndexToWorldCoord(2, 3), 0.05f);
    }

    void invalidate(int x, int y)
    {
       
        orientations[x, y] = (orientations[x, y] + 1)%4;
        Debug.Log(pathTrace(puzzlepieces, orientations, 0, 0));
    }
}
