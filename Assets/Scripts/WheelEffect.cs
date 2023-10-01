using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelEffect : MonoBehaviour
{
    [SerializeField] private WheelCollider[] wheels;
    [SerializeField] private ParticleSystem[] wheelsSmoke;

    [SerializeField] private float forwardSlipLimit;
    [SerializeField] private float sideWaySlipLimit;

    [SerializeField] private AudioSource audio;

    [SerializeField] private GameObject skidPrefab;

    private WheelHit wheelHit;
    private Transform[] skidTrail;

    private void Start()
    {
        skidTrail = new Transform[wheels.Length];
    }
    private void Update()
    {
        bool isSleep=false;
        for(int i=0;i<wheels.Length;i++)
        {
            wheels[i].GetGroundHit(out wheelHit);

            if(wheels[i].isGrounded==true)
            {
               if(wheelHit.forwardSlip>forwardSlipLimit || wheelHit.sidewaysSlip>sideWaySlipLimit)
                {
                    if(skidTrail[i]==null)
                    {
                        skidTrail[i] = Instantiate(skidPrefab).transform;
                    }

                    if(audio.isPlaying==false)
                    {
                        audio.Play();
                    }

                    if(skidTrail!=null)
                    {
                        skidTrail[i].position = wheels[i].transform.position - wheelHit.normal * wheels[i].radius;
                        skidTrail[i].forward = -wheelHit.normal;

                        wheelsSmoke[i].transform.position = skidTrail[i].position;
                        wheelsSmoke[i].Emit(10);
                    }

                    isSleep = true;
                    
                    continue;
                }
            }
            skidTrail[i] = null;
            wheelsSmoke[i].Stop();
        }
        if (isSleep == false) audio.Stop();
    }
}
