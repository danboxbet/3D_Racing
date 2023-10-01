using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarChassis : MonoBehaviour
{
    [SerializeField] private WheelAxell[] wheelAxells;
    [SerializeField] private float wheelBaseLength;

    [SerializeField] private Transform centerOfMass;

    [Header("AngularDrag")]
    [SerializeField] private float angularDragMin;
    [SerializeField] private float angularDragMax;
    [SerializeField] private float angularDragFactor;

    [Header("DownForce")]
    [SerializeField] private float downForceMin;
    [SerializeField] private float downForceMax;
    [SerializeField] private float downForceFactor;

    public float LinearVelocity => rigidbody.velocity.magnitude * 3.6f;

    public float motorTorgue;
    public float breakTorgue;
    public float steerAngle;

    private new Rigidbody rigidbody;
    public Rigidbody Rigidbody => rigidbody == null ? GetComponent<Rigidbody>():rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        if(centerOfMass!=null)
        {
            rigidbody.centerOfMass = centerOfMass.localPosition;
        }
        for(int i=0;i<wheelAxells.Length;i++)
        {
            wheelAxells[i].ConfigureVehicleSubsteps(50, 50, 50);
        }
    }
    private void FixedUpdate()
    {
        UpdateAngularDrag();
        UpdateDownForce();
        UpdateWheelAxies();
    }
    public float GetAvarageRpm()
    {
        float sum = 0;

        for(int i=0;i<wheelAxells.Length;i++)
        {
            sum += wheelAxells[i].GetAvarageRpm();
        }
        return sum / wheelAxells.Length;
    }
    public float GetWheelSpeed()
    {
        return GetAvarageRpm()*wheelAxells[0].GetRadius() * 2 * 0.1885f;
    }
    private void UpdateDownForce()
    {
       float downForce= Mathf.Clamp(downForceFactor * LinearVelocity,downForceMin, downForceMax);
        rigidbody.AddForce(-transform.up * downForce);
    }

    private void UpdateAngularDrag()
    {
        rigidbody.angularDrag = Mathf.Clamp(angularDragFactor * LinearVelocity, angularDragMin, angularDragMax);
    }

    private void UpdateWheelAxies()
    {
        int amountMorotWheel = 0;

        for(int i=0;i<wheelAxells.Length;i++)
        {
            if(wheelAxells[i].IsMotor==true)
            {
                amountMorotWheel += 2;
            }
        }
        for(int i=0; i<wheelAxells.Length;i++)
        {
            wheelAxells[i].Update();

            wheelAxells[i].ApplyMotorTorque(motorTorgue/amountMorotWheel);
            wheelAxells[i].ApplySteerAngle(steerAngle,wheelBaseLength);
            wheelAxells[i].ApplyBreakTorque(breakTorgue);
        }
    }

    public void Reset()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
