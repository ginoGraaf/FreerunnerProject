using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MatDetection : MonoBehaviour
{
    MatModel[] mats;

    [ReadOnly]
   public int lookAhead;
    [SerializeField]
    int AmountsOfMats;
     
    private void Start()
    {
        mats = new MatModel[AmountsOfMats];
        for (int i = 0; i < AmountsOfMats; i++)
        {
            mats[i] = new MatModel { LightsOn = false };
        }
    }

    // Update is called once per frame
    void Update()
    {
        //als niek klaar is kan ik zijn coroutine gebruiken voor dit.
    }
}
