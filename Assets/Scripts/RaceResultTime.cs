using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class RaceResultTime : MonoBehaviour, IDependency<RaceTimeTracker>, IDependency<RaceStateTracker>
{
    public const string SaveMark = "_player_best_time";

    public event UnityAction UpdateResults;
   

    [SerializeField] private float goldTime;

    private RaceStateTracker raceStateTracker;
    private RaceTimeTracker raceTimeTracker;

    private float playerRecordTime;
    private float currentTime;

    public float GoldTime => goldTime;
    public float PlayerRecordTime => playerRecordTime;
    public float CurrentTime => currentTime;
    public bool RecordWasSet => playerRecordTime != 0;

    public void Construct(RaceTimeTracker obj)
    {
        raceTimeTracker = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }
    private void Awake()
    {
        Load();
    }
    private void Start()
    {
        raceStateTracker.Completed += OnRaceCompleted;
    }

    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceCompleted()
    {
        float absiluteRecord = GetAbsoluteRecord();
        if(raceTimeTracker.CurrentTime<absiluteRecord || playerRecordTime==0)
        {
            playerRecordTime = raceTimeTracker.CurrentTime;

            Save();
           
        }

        currentTime = raceTimeTracker.CurrentTime;
        UpdateResults?.Invoke();
    }
    public float GetAbsoluteRecord()
    {
        if(playerRecordTime<goldTime && playerRecordTime!=0)
        {
            return playerRecordTime;
        }
        else
        {
            return goldTime;
        }
    }
    private void Load()
    {
        playerRecordTime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + SaveMark, 0);
    }
    private void Save()
    {
        PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + SaveMark, playerRecordTime);
    }
}
