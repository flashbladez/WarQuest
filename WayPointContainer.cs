using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WarQuest.Characters
{
    public class WayPointContainer : MonoBehaviour
    {
        [SerializeField] bool white = false;
        [SerializeField] bool black = false;
        [SerializeField] bool green = true;
        [SerializeField] bool magenta = false;
        [SerializeField] bool yellow = false;
        [SerializeField] bool cyan = false;
        [SerializeField] bool red = false;
        [SerializeField] bool blue = false;
        [SerializeField] bool grey = false;
        [SerializeField] bool orange = false;
        [SerializeField] string characterType = null;
        
        void OnDrawGizmos()
        {
           // var newColour = myColour.ToString();
            Vector3 firstPosition = transform.GetChild(0).position;
            Vector3 previousPosition = firstPosition;

            foreach (Transform waypoint in transform)
            {
                if (white)
                {
                    Gizmos.color = Color.white;
                }
                if (black)
                {
                    Gizmos.color = Color.black;
                }
                if (green)
                {
                    Gizmos.color = Color.green;
                }
                if (magenta)
                {
                    Gizmos.color = Color.magenta;
                }
                if (yellow)
                {
                    Gizmos.color = Color.yellow;
                }
                if (cyan)
                {
                    Gizmos.color = Color.cyan;
                }
                if (red)
                {
                    Gizmos.color = Color.red;
                }
                if (blue)
                {
                    Gizmos.color = Color.blue;
                }
                if (grey)
                {
                    Gizmos.color = Color.grey;
                }
                if (orange)
                {
                    Color _orange = new Color(1.0f, 0.3f, 0.1f);
                    Gizmos.color = _orange;
                }

                Gizmos.DrawSphere(waypoint.position, .2f);
                Gizmos.DrawLine(previousPosition, waypoint.position);
                previousPosition = waypoint.position;
            }
            Gizmos.DrawLine(previousPosition, firstPosition);
        }
    }
}
