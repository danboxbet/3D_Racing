using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatedTrackPoint : TrackPoint
{
    [SerializeField] private GameObject hint;

    private void Start()
    {
        hint.SetActive(isTarger);
    }
    protected override void OnPassed()
    {
        hint.SetActive(false);
    }
    protected override void OnAssignAsTarget()
    {
        hint.SetActive(true);
    }
}
