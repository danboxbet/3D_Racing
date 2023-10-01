using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PostCompletedRecord : UIRaceRecordTime
{

    protected override void Start()
    {
        goldRecordObject.SetActive(false);
        playerRecordObject.SetActive(false);
        raceStateTracker.Completed += OnReaceCompleted;
    }
    
    private void OnDestroy()
    {
        raceStateTracker.Completed -= OnReaceCompleted;
    }
    protected override void OnRaceStart()
    {
        base.OnRaceStart();
    }
    protected override void OnReaceCompleted()
    {
        OnRaceStart();
        goldRecordObject.SetActive(true);
        playerRecordObject.SetActive(true);
    }
}
