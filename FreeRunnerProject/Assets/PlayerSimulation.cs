using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSimulation : MonoBehaviour
{
    List<Transform> points = new List<Transform>();
    Transform currentPoint;
    int nextPoint = 0;
    [SerializeField]
    float playerSpeed = 20;
    float speed = 0;
    // Start is called before the first frame update
    [SerializeField]
    bool Automatic;
    [SerializeField]
    Transform PlayerTransform;
    [SerializeField]
    Rigidbody rig;
    void Start()
    {
        points = boardBuilder.instance.path_objs;

        currentPoint = points[nextPoint];
    }

    // Update is called once per frame
    void Update()
    {
        speed = playerSpeed * Time.deltaTime;
        Pathfollowing();
    }

    void Pathfollowing()
    {
        ReachPoint();
        FolowPath();
    }

     bool LastPoint()
    {
        if(points.Count<= nextPoint && boardBuilder.instance.isclosed)
        {
            nextPoint = 0;
            currentPoint = points[nextPoint];
            return false;
        }
        return true;
    }
    void ReachPoint()
    {
        if(Vector3.Distance(PlayerTransform.position,currentPoint.position)<0.1f)
        {
            nextPoint++;
            if (LastPoint())
            {
                currentPoint = points[nextPoint];
            }
        }
    }

    void FolowPath()
    {
        PlayerTransform.position = Vector3.MoveTowards(PlayerTransform.position, currentPoint.position, speed);
    }
}
