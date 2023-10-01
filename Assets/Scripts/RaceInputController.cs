using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceInputController : MonoBehaviour,IDependency<RaceStateTracker>,IDependency<CarInputControl>
{
    private CarInputControl carControl;
    private RaceStateTracker raceStateTrcker;
    public void Construct(CarInputControl obj)
    {
        carControl = obj;
    }
    public void Construct(RaceStateTracker obj)
    {
        raceStateTrcker = obj;
    }
    private void Start()
    {
        raceStateTrcker.Started += OnRaceStarted;
        raceStateTrcker.Completed += OnRaceFinished;

        carControl.enabled = false;
    }
    private void OnDestroy()
    {
        raceStateTrcker.Started -= OnRaceStarted;
        raceStateTrcker.Completed -= OnRaceFinished;
    }
    private void OnRaceFinished()
    {
        carControl.Stop();
        carControl.enabled = false;
    }

    private void OnRaceStarted()
    {
        carControl.enabled = true;
    }

   
}
