using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boardBuilder : MonoBehaviour {
    public Color raycolor = Color.white;
    public List<Transform> path_objs = new List<Transform>();
    Transform[] theArray;
    public float size = 0.3f;
    public bool isclosed = false;
    public static boardBuilder instance { get; set; }
    public List<Transform> Path_objs
    {
        get
        {
            return path_objs;
        }

        set
        {
            path_objs = value;
        }
    }
    void Awake()
    {
        instance = this;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = raycolor;
        theArray = GetComponentsInChildren<Transform>();
        Path_objs.Clear();

        foreach (Transform objTransform in theArray)
        {
            if (objTransform != this.transform)
            {
        

                    Path_objs.Add(objTransform);

            }
        }
        for (int i = 0; i < Path_objs.Count; i++)
        {
            Vector3 position = Path_objs[i].position;
            if (i > 0)
            {
                Vector3 previous = Path_objs[i - 1].position;

                Gizmos.DrawLine(previous, position);

                Gizmos.DrawWireSphere(position, size);
            }
        }
        if (isclosed)
        {
            int total = Path_objs.Count - 1;
            Gizmos.DrawLine(Path_objs[total].position, Path_objs[0].position);
        }
    }
}
