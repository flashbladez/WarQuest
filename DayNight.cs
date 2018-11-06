using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace WarQuest.Environment
{
    public class DayNight : MonoBehaviour
    {
        [SerializeField] float lengthOfDayHours;
        [SerializeField] float skyBoxSpeed = 1;

        float numberOfMinsDay;
        float numberOfSecsFay;
        float rotateByPerSec;
        Light lights;
        float totalLight = 1f;
        bool isDay = true;
        void Start()
        {
            lights = GetComponent<Light>();
            rotateByPerSec = (lengthOfDayHours * 60) * 60;
           // totalLight = lights.intensity =1f - (1 / rotateByPerSec);
        }

  
        void Update()
        {
            //  if (lights.intensity > 0.15f && isDay)
            //  {
            //      lights.intensity = totalLight - (1 / rotateByPerSec) * Time.deltaTime;
            //     totalLight -= (1 / rotateByPerSec) * Time.deltaTime;

            // }

            // if (lights.intensity < 1.0f && !isDay)
            //  {
            //   lights.intensity = totalLight + (1 / rotateByPerSec) * Time.deltaTime;
            //  totalLight += (1 / rotateByPerSec) * Time.deltaTime;

            //   }
            // if (lights.intensity <= 0.15f && isDay)
            // {
            //    isDay = false;

            // }else if (lights.intensity >= 1.0f && !isDay)
            //  {
            //    isDay = true;

            //  }


            RenderSettings.skybox.SetFloat("_Rotation", Time.time * skyBoxSpeed);
            transform.Rotate(Vector3.right * 360f / rotateByPerSec) ;
        }

        void SpinLight()
        {

        }
    }
}
