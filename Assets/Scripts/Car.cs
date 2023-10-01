using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CarChassis))]
public class Car : MonoBehaviour
{
    public event UnityAction<string> GearChanged;

    [SerializeField] private float maxSteerAngle;
    [SerializeField] private float maxBrakeTorque;

    [Header("Engine")]
    [SerializeField] private AnimationCurve engineTorqueCurve;
    [SerializeField] private float engineMaxTorque;
    [SerializeField] private float engineTorque;
    [SerializeField] private float engineRpm;
    [SerializeField] private float engineMinRpm;
    [SerializeField] private float engineMaxRpm;

    [Header("Gearbox")]
    [SerializeField] private float[] gears;
    [SerializeField] private float finalDriveRatio;
    [SerializeField] private float selectedGear;
    [SerializeField] private float rearGear;
    [SerializeField] private int selectedGearIndex;

    [SerializeField] private float upShiftEngineRpm;
    [SerializeField] private float downShiftEngineRpm;

    
    [SerializeField] private float maxSpeed;
    private CarChassis chassis;
    public Rigidbody Rigidbody => chassis == null ? GetComponent<CarChassis>().Rigidbody:chassis.Rigidbody;
    public float LinearVelocity => chassis.LinearVelocity;
    public float NormalizeLinearVelocity => chassis.LinearVelocity/maxSpeed;
    public float WheelSpeed => chassis.GetWheelSpeed();

    public float EngineRpm => engineRpm;
    public float EngineMaxRpm => engineMaxRpm;
    public float MaxSpeed => maxSpeed;

    public float ThrottControl;
    public float SteerControl;
    public float BrakeControl;
    public float HandBrakeControl;

    private void Start()
    {
        chassis = GetComponent<CarChassis>();
    }
    private void Update()
    {
        UpdateEngineTorque();

        AutoGearShift();
       
        if (LinearVelocity >= maxSpeed) engineTorque = 0;

        chassis.motorTorgue = ThrottControl* engineTorque;
        chassis.breakTorgue = BrakeControl* maxBrakeTorque;
        chassis.steerAngle = SteerControl* maxSteerAngle;
    }
    private void AutoGearShift()
    {
        if (selectedGear < 0) return;

        if (engineRpm >= upShiftEngineRpm) UpGear();

        if(engineRpm<downShiftEngineRpm)
        {
            DownGear();
        }

       
    }
    public string GetSelectedGearName()
    {
        if (selectedGear == rearGear) return "R";
        if (selectedGear == 0) return "N";
        return (selectedGearIndex + 1).ToString();
    }
    public void UpGear()
    {
        ShiftGear(selectedGearIndex + 1);
    }
    public void DownGear()
    {
        ShiftGear(selectedGearIndex - 1);
    }
    public void ShiftToReverseGear()
    {
        selectedGear = rearGear;

        GearChanged?.Invoke(GetSelectedGearName());

    }
    public void ShiftToFirstGear()
    {
        ShiftGear(0);
    }
    public void ShiftToNetral()
    {
        selectedGear = 0;
        GearChanged?.Invoke(GetSelectedGearName());
    }
    private void ShiftGear(int gearIndex)
    {
        gearIndex = Mathf.Clamp(gearIndex, 0, gears.Length - 1);
        selectedGear = gears[gearIndex];
        selectedGearIndex = gearIndex;

        GearChanged?.Invoke(GetSelectedGearName());

    }
    private void UpdateEngineTorque()
    {
        engineRpm = engineMinRpm + Mathf.Abs(chassis.GetAvarageRpm()*selectedGear*finalDriveRatio);
        engineRpm = Mathf.Clamp(engineRpm, engineMinRpm, engineMaxRpm);

        engineTorque = engineTorqueCurve.Evaluate(engineRpm / engineMaxRpm) * engineMaxTorque*finalDriveRatio*Mathf.Sign(selectedGear)*gears[0];
    }
    public void Reset()
    {
        chassis.Reset();

        chassis.motorTorgue = 0;
        chassis.breakTorgue = 0;
        chassis.steerAngle = 0;

        ThrottControl = 0;
        BrakeControl = 0;
        SteerControl = 0;

    }
    public void Respawn(Vector3 position,Quaternion rotation)
    {
        Reset();
        transform.position = position;
        transform.rotation = rotation;
    }
}
