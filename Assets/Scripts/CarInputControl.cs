using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField] [Range(0.0f, 1.0f)] private float autoBreakStrangth = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbreakAxis;

    private void Update()
    {
        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBreak();
        UpdateSteer();


        UpdateAutoBrake();
    }

    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed/car.MaxSpeed) * horizontalAxis;
    }

   

    private void UpdateThrottleAndBreak()
    {
       
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottControl = 0;
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

        if(verticalAxis<0 && wheelSpeed>-0.5f && wheelSpeed<=0.5f)
        {
            car.ShiftToReverseGear();

        }
        if(verticalAxis>0 && wheelSpeed>-0.5f && wheelSpeed <0.5f)
        {
            car.ShiftToFirstGear();
        }
    }

    public void Reset()
    {
        verticalAxis = 0;
        horizontalAxis = 0;
        handbreakAxis = 0;

        car.ThrottControl = 0;
        car.SteerControl = 0;
        car.BrakeControl = 0;
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbreakAxis = Input.GetAxis("Jump");
    }

    private void UpdateAutoBrake()
    {
        if(verticalAxis==0)
        {
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed * autoBreakStrangth);
        }
    }
    public void Stop()
    {
        Reset();

        car.BrakeControl = 1;
    }
}
