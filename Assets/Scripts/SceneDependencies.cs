using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDependency<T>
{
    void Construct(T obj);
}
public class SceneDependencies : Dependency
{
    [SerializeField] private Pauser pauser;
    [SerializeField] private RaceResultTime raceResultTime;
    [SerializeField] private RaceTimeTracker raceTimeTracker;
    [SerializeField] private RaceStateTracker raceStateTracker;
    [SerializeField] private CarInputControl carInputControl;
    [SerializeField] private TrackpointCircuit trackpointCircuit;
    [SerializeField] private Car car;
    [SerializeField] private CarCameraController carCameraController;

    protected override void Bind(MonoBehaviour mono)
    {
        Bind<Pauser>(pauser, mono);
        Bind<RaceResultTime>(raceResultTime, mono);
        Bind<RaceStateTracker>(raceStateTracker, mono);
        Bind<RaceTimeTracker>(raceTimeTracker, mono);
        Bind<CarInputControl>(carInputControl, mono);
        Bind<TrackpointCircuit>(trackpointCircuit, mono);
        Bind<Car>(car, mono);
        Bind<CarCameraController>(carCameraController, mono);
       
    
    }
    
    private void Awake()
    {
        FindAllObjectToBind();   
    }
}
