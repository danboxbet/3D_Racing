using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}
public class RaceStateTracker : MonoBehaviour,IDependency<TrackpointCircuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;

    private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Timer countDownTimer;
    [SerializeField] private int lapsToComplete;

    public Timer CountDownTimer => countDownTimer;
    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }
    public void Construct(TrackpointCircuit trackpointCircuit)
    {
        this.trackpointCircuit = trackpointCircuit;
    }
    private void Start()
    {
        
        StartState(RaceState.Preparation);

        countDownTimer.enabled = false;
        countDownTimer.Finished += OnCountDownTimerFinished;

        trackpointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackpointCircuit.LapCompleted += OnLapCompleted;
    }

   

    private void OnDestroy()
    {
        countDownTimer.Finished -= OnCountDownTimerFinished;
        trackpointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackpointCircuit.LapCompleted -= OnLapCompleted;
    }
    private void OnCountDownTimerFinished()
    {
        StartRace();
    }
    private void OnLapCompleted(int lapAmount)
    {
        if(trackpointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }
        
        if(trackpointCircuit.Type==TrackType.Circular)
        {
            if (lapAmount == lapsToComplete) CompleteRace();
            else CompleteLap(lapAmount);
        }
    }
    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed.Invoke(trackPoint);
    }

    public void LaunchPrepatationStarted()
    {
        if (state != RaceState.Preparation) return;

        StartState(RaceState.CountDown);

        countDownTimer.enabled = true;
        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.CountDown) return;

        StartState(RaceState.Race);
        Started?.Invoke();
    }

    private void CompleteRace()
    {
        if (state != RaceState.Race) return;

        StartState(RaceState.Passed);
        Completed?.Invoke();
    }
    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

   
}
