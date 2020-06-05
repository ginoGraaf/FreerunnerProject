using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public ChaseLight[] ChaseLights;
    [SerializeField] private int firstDangerPoint = 11;
    [SerializeField] private int dangerZoneLength = 5;
    float timeLeft = 2.0f;

    void Start()
    {
        SetupDangerZone();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            MoveDangerZone();
            Debug.Log("move");
            timeLeft = 2.0f;
        }
    }

    private void SetupDangerZone()
    {
        for (int _i = 0; _i < dangerZoneLength; _i++)
        {
            int _lightToAdd = firstDangerPoint - _i;
            if (_lightToAdd < 0) // prevent negative numbers
            {
                _lightToAdd += ChaseLights.Length;
            }
           ChaseLights[_lightToAdd].SetState(1);
        }
    }

    private void MoveDangerZone()
    {
        // remove last light from dangerzone
        int _lightToRemove = firstDangerPoint - (dangerZoneLength - 1);
        if (_lightToRemove < 0)
        {
            _lightToRemove += ChaseLights.Length;
        }
        ChaseLights[_lightToRemove].SetState(2);

        // add next light to dangerzone
        firstDangerPoint++;
        if (firstDangerPoint > ChaseLights.Length - 1)
        {
            firstDangerPoint = 0;
        }
        ChaseLights[firstDangerPoint].SetState(1);
    }
}