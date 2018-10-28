using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WarQuest.Characters;

namespace WarQuest.Environment
{
    public class ZoneDisplayText : MonoBehaviour
    {
        [SerializeField] Text zoneText;

        void Start()
        {
            Invoke("ClearZoneText", 5);
        }


        public void DisplayZoneText(string zoneInText)
        {
            GetComponent<Text>().text = zoneInText;
            Invoke("ClearZoneText", 5);
        }

        void ClearZoneText()
        {
            GetComponent<Text>().text = "";
            CancelInvoke("ClearZoneText");
        }
    }
}
