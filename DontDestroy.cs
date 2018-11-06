using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WarQuest.Environment
{
    public class DontDestroy : MonoBehaviour
    {
        void Awake()
        {
           
            DontDestroyOnLoad(this.gameObject);
           
           
        }
    }
}
