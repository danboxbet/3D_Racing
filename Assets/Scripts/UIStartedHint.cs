using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartedHint : MonoBehaviour, IDependency<RaceStateTracker>
{
    [SerializeField] private GameObject startedHint;
    private RaceStateTracker raceStateTracker;

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }

    void Start()
    {
        raceStateTracker.PreparationStarted += ActivateHint;
        startedHint.SetActive(true);
    }
    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= ActivateHint;
    }
    private void ActivateHint()
    {
        startedHint.SetActive(false);
    }
}
