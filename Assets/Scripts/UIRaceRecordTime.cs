using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRaceRecordTime : MonoBehaviour,IDependency<RaceResultTime>,IDependency<RaceStateTracker>
{
    [SerializeField] protected GameObject goldRecordObject;
    [SerializeField] protected GameObject playerRecordObject;
    [SerializeField] private Text goldRecordTime;
    [SerializeField] private Text playerRecordTime;

   
    protected RaceResultTime raceResultTime;
    protected RaceStateTracker raceStateTracker;

    public void Construct(RaceResultTime obj)
    {
        raceResultTime = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }
    protected virtual void Start()
    {
        raceStateTracker.Started += OnRaceStart;
        raceStateTracker.Completed += OnReaceCompleted;

        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }
    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStart;
        raceStateTracker.Completed -= OnReaceCompleted;
    }
    protected virtual void OnReaceCompleted()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
    }

    protected virtual void OnRaceStart()
    {
        if (raceResultTime.PlayerRecordTime > raceResultTime.GoldTime || raceResultTime.RecordWasSet == false)
        {
            goldRecordObject.SetActive(true);
            goldRecordTime.text = StringTime.SecondToTimeString(raceResultTime.GoldTime);
        }
        if (raceResultTime.RecordWasSet == true)
        {
            playerRecordObject.SetActive(true);
            playerRecordTime.text = StringTime.SecondToTimeString(raceResultTime.PlayerRecordTime);
        }
    }
}
