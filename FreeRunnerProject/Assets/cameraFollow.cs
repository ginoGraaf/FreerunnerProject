using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    [SerializeField]
    List<CamPoints> camPoints = new List<CamPoints>();
    Vector3 target_Offset = new Vector3(0,0,0);
    [SerializeField]
    float speed;
    int index = 0;
    // Update is called once per frame
    void Update()
    {

            Move(camPoints[index].FollowObject.transform.position, camPoints[index].PivotPoint.transform.position);
        ChangeCamePoint();
    }

  void ChangeCamePoint()
    {
        if(Input.GetKeyUp(KeyCode.R))
        {
            index++;
            if(index>=camPoints.Count)
            {
                index = 0;
            }
        }
    }

    void Move(Vector3 lookAt, Vector3 Pivot)
    {
        if(lookAt==null && Pivot==null)
        {
            return;
        }
        Vector3 dirction = (lookAt - Pivot).normalized;
        Quaternion lookRoation = Quaternion.LookRotation(dirction);
        // lookRoation.x = transform.rotation.x;
        // lookRoation.z = transform.rotation.z;

        transform.rotation = Quaternion.Slerp(transform.rotation, lookRoation, Time.deltaTime * 5);

        transform.position = Vector3.Slerp(transform.position, Pivot, Time.deltaTime * speed);
    }

}
[System.Serializable]
public class CamPoints
{
    public GameObject FollowObject;
    public GameObject PivotPoint;
}
