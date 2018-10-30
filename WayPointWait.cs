using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WarQuest.Characters
{
    public class WayPointWait : MonoBehaviour
    {

        [SerializeField] float timeToWait = 5f;

        public float TimeToWait
        {
            get { return timeToWait; }
            set { timeToWait = value; }
        }
    }
}
