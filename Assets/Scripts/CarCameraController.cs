using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCameraController : MonoBehaviour, IDependency<Car>, IDependency<RaceStateTracker>
{
    [SerializeField] private Car car;
    [SerializeField] private new Camera camera;
    [SerializeField] private CameraFollow follower;
    [SerializeField] private CameraShaker shaker;
    [SerializeField] private CameraFovCorrector fovCorrector;
    [SerializeField] private CameraPathFollower pathFollower;

    [SerializeField] private RaceStateTracker raceStateTracker;
    public void Construct(Car obj)
    {
        car = obj;
    }

    public void Construct(RaceStateTracker obj)
    {
        raceStateTracker = obj;
    }
    private void Awake()
    {
        follower.SetProperties(car, camera);
        shaker.SetProperties(car, camera);
        fovCorrector.SetProperties(car, camera);
    }
    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Completed += OnCompleted;

        follower.enabled = false;
        pathFollower.enabled = true;
    }
    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Completed -= OnCompleted;

    }
    private void OnCompleted()
    {
        pathFollower.enabled = true;
        pathFollower.StartMoveToNearestPoint();
        pathFollower.SetLookTarget(car.transform);

        follower.enabled = false;
    }

    private void OnPreparationStarted()
    {
        follower.enabled = true;
        pathFollower.enabled = false;
    }

   
}
