using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResultSaver : MonoBehaviour,IDependency<RaceStateTracker>, IDependency<RaceResultTime>
{
    [SerializeField] private float firstTime;
    [SerializeField] private float secondTime;
    [SerializeField] private float thirdTime;

    private int cups;

    private List<float> allTimes=new List<float>();
    private string sceneName;

    private RaceStateTracker raceState;
    private RaceResultTime resultTime;
    public void Construct(RaceStateTracker obj)
    {
        raceState = obj;
    }
    public void Construct(RaceResultTime obj)
    {
        resultTime = obj;
    }

    private void Start()
    {
        Debug.Log("asd");
        resultTime.UpdateResults += WriteRaceResult;
        sceneName = SceneManager.GetActiveScene().name;

        allTimes.Add(firstTime);
        allTimes.Add(secondTime);
        allTimes.Add(thirdTime);

    }
    private void OnDestroy()
    {
        resultTime.UpdateResults -= WriteRaceResult;
    }
    private void WriteRaceResult()
    {
       
        for(int i=0; i<allTimes.Count;i++)
        {
           
            if (resultTime.CurrentTime < allTimes[i] && resultTime.CurrentTime==resultTime.PlayerRecordTime)
            {
              
                cups = allTimes.Count - i;
                SaveResult(cups);
                break;
            }
           
        }
    }
    private void SaveResult(int cups)
    {
        
        PlayerPrefs.SetInt(sceneName + "result", cups);
    }
}
