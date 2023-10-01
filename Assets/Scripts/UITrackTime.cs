using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackTime : MonoBehaviour, IDependency<RaceStateTracker>, IDependency<RaceTimeTracker>
{
    [SerializeField] private Text text;

    private RaceStateTracker raceStateTracker;
    private RaceTimeTracker raceTimeTracker;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }
    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;

    }
    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void OnRaceCompleted()
    {
        text.enabled = false;
        enabled = false;
    }
    private void Update()
    {
        text.text = StringTime.SecondToTimeString(raceTimeTracker.CurrentTime);
    }


}
