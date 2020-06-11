using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool gameActive = false;

    public ChaseLight[] ChaseLights;

    [Header("Danger zone")]
    [SerializeField] private int firstDangerPoint = 11;
    [SerializeField] private int dangerZoneLength = 5;

    [Header("Interval")]
    [SerializeField] private float startInterval = 3.0f;
    [SerializeField] private float IntervalFactor = 0.97f;
    [SerializeField] private float interval;
    [SerializeField] private float timeLeft;

    void Start()
    {
        Debug.Log("Don't forget to check Game Active in the game controller ;)");
        SetupDangerZone();
        interval = startInterval;
        timeLeft = startInterval;
    }

    void Update()
    {
        if (gameActive)
        {
            Timer();
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
           ChaseLights[_lightToAdd].SetState(ChaseLight.state.DANGER);
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
        ChaseLights[_lightToRemove].SetState(ChaseLight.state.ON);

        // add next light to dangerzone
        firstDangerPoint++;
        if (firstDangerPoint > ChaseLights.Length - 1)
        {
            firstDangerPoint = 0;
        }
        ChaseLights[firstDangerPoint].SetState(ChaseLight.state.DANGER);
    }

    private void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            MoveDangerZone();
            Debug.Log("move");
            timeLeft = interval;
            interval = interval * IntervalFactor;
        }
    }
}