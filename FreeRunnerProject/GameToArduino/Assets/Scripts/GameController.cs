using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    public UnityEvent GameStopped = new UnityEvent();
    public Settings settings;
    private ArduinoConnection ard;
    public bool gameActive = false;

    [HideInInspector] public enum state {ON,OFF,DANGER, BLINK}; 
    public state[] ChaseLights;

    [Header("Danger zone")]
    [SerializeField] private int firstDangerPoint = 0;
    [SerializeField] private int dangerZoneLength = 1;
    [SerializeField] private int blinkZoneLength = 1;
    public int _blinkLight;

    [Header("Interval")]
    [SerializeField] private float startInterval = 3.0f;
    [SerializeField] private float IntervalFactor = 0.97f;
    [SerializeField] private float interval;
    [SerializeField] private float timeLeft;
    private IEnumerator blinkRoutine;

    
    void Start()
    {
        blinkRoutine = Blink(); 
        ard = transform.GetComponent<ArduinoConnection>();
        ard.NumberOfNodes = Int32.Parse(settings.numberOfNodes);
        ChaseLights = new state[Int32.Parse(settings.numberOfNodes)];
        ard.Connect();
        
        interval = startInterval;
        timeLeft = startInterval;

        SetupDangerZone();
    }

    void Update()
    {
        if (gameActive)
        {
            Timer();
        }
    }

    public void StartGame()
    {
        gameActive = true;
        StartCoroutine(blinkRoutine);
    }

    public void StopGame()
    {
        gameActive = false;
        GameStopped.Invoke();
        StopCoroutine(blinkRoutine);
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
           ChaseLights[_lightToAdd] = state.DANGER;
           ard.SetLight(_lightToAdd, 'R');
        }
        for (int _i = 0; _i < blinkZoneLength; _i++)
        {
             _blinkLight = firstDangerPoint + _i + 1;

            if (_blinkLight >= ChaseLights.Length)
            {
                _blinkLight = _blinkLight - ChaseLights.Length;
            }
            ChaseLights[_blinkLight]=state.BLINK;
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
        ChaseLights[_lightToRemove]=state.ON;
        ard.SetLight(_lightToRemove, '0');

        // add next light to dangerzone
        firstDangerPoint++;
        if (firstDangerPoint > ChaseLights.Length - 1)
        {
            firstDangerPoint = 0;
        }
        ChaseLights[firstDangerPoint]=state.DANGER;
        if(ard.ButtonStatus[firstDangerPoint])
        {
            StopGame();
            Debug.Log("GAME OVER");
        }
         ard.SetLight(firstDangerPoint, 'R');

         _blinkLight = firstDangerPoint + blinkZoneLength;
        if (_blinkLight >= ChaseLights.Length)
        {
            _blinkLight = _blinkLight - ChaseLights.Length;
        }
        ChaseLights[_blinkLight]=state.BLINK;
    }

    IEnumerator Blink()
    {
        bool on = true;
        while(true)
        {
            if(on)
            {
                ard.SetLight(_blinkLight, '0');
                on = false;
            }
            else
            {
                ard.SetLight(_blinkLight, 'R');
                on = true;
            }
            yield return new WaitForSeconds(0.8f);
        }
    }

    private void Timer()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            MoveDangerZone();
            
            timeLeft = interval;
            interval = interval * IntervalFactor;
        }
    }
}