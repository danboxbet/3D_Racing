using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrackPoint : MonoBehaviour
{
    public event UnityAction<TrackPoint> Triggered;

    protected virtual void OnPassed() { }
    protected virtual void OnAssignAsTarget() { }

    public TrackPoint Next;
    public bool IsFirst;
    public bool IsLast;

    protected bool isTarger;
    public bool IsTarget => isTarger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.GetComponent<Car>() == null)      
            return;

        Triggered?.Invoke(this);

    }

    public void Passed()
    {
        isTarger = false;
        OnPassed();
    }

    public void AssignAsTarget()
    {
        isTarger = true;
        OnAssignAsTarget();
    }
    public void ResetValue()
    {
        Next = null;
        IsFirst = false;
        IsLast = false;
    }
}
