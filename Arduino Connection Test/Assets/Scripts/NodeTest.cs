using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTest : MonoBehaviour
{
    private ArduinoConnection ard;

    // Start is called before the first frame update
    void Start()
    {
        ard = transform.GetComponent<ArduinoConnection>();
        //StartCoroutine(ChangeLight());
        StartCoroutine(BlinkLight());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ChangeLight()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            ard.SetLight(Random.Range(0, ard.NumberOfNodes), 'R');
            ard.SetLight(Random.Range(0, ard.NumberOfNodes), '0');
        }
    }

    IEnumerator BlinkLight()
    {
        int counter = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.05f);
            if (counter == 0)
            {
                ard.SetLight(0, 'R');
                counter++;
            }
            else
            {
                ard.SetLight(0, '0');
                counter++;
            }
            if(counter > 1)
            {
                counter = 0;
            }
        }
    }
}
