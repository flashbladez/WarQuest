﻿using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using WarQuest.Characters;//so we can detect by type
using System;

namespace WarQuest.CameraUI
{
    public class CameraRaycaster : MonoBehaviour
    {
        // INSPECTOR PROPERTIES RENDERED BY CUSTOM EDITOR SCRIPT
        [SerializeField] Texture2D walkCursor = null;
        [SerializeField] Texture2D enemyCursor = null;
        [SerializeField] Vector2 cursorHotspot = new Vector2(96, 96);

        float maxRaycastDepth = 1000f;//Hard coded value
        Rect currentScreenRect = new Rect() ;
        const int POTENTIALLY_WALKABLE_LAYER = 10;
        GameObject gameObjectHit = null;
        public delegate void OnMouseOverEnemy(EnemyAI enemy);
        public event OnMouseOverEnemy onMouseOverEnemy;

        public delegate void OnMouseOverTerrain(Vector3 destination);
        public event OnMouseOverTerrain onMouseOverPotentiallyWalkable;

       

        void Update()
        {
            currentScreenRect = new Rect(0, 0, Screen.width, Screen.height);
            // Check if pointer is over an interactable UI element
         //   if (EventSystem.current.IsPointerOverGameObject())
          //  {
                //implement UI Interaction
              
           // }
          // else
           // {
                PerformRaycasts();
           // }

        }

        void PerformRaycasts()
        {
            if (currentScreenRect.Contains(Input.mousePosition))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
       
                    if (RaycastForEnemy(ray))
                    {
                        return;
                    }
                    else if (RaycastForWalkable(ray))
                    {
                        return;
                    }
            }
            else
            {
                return;
            }     
        }


        bool RaycastForEnemy(Ray ray)
        {
          if (GameObject.FindGameObjectWithTag("Player"))
          {
                RaycastHit hitInfo;
                Physics.Raycast(ray, out hitInfo, maxRaycastDepth);

               if (!hitInfo.collider)
                { 
                   return false;
               }
               else
               {       
                    gameObjectHit = hitInfo.collider.gameObject;
               }

                var enemyHit = gameObjectHit.GetComponent<EnemyAI>();
               
                if (enemyHit)
                {
                   // print(enemyHit);
                    Cursor.SetCursor(enemyCursor, cursorHotspot, CursorMode.Auto);
                    onMouseOverEnemy(enemyHit);
                return true;
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        bool RaycastForWalkable(Ray ray)
        {
            RaycastHit hitInfo;
            LayerMask potentiallyWalkableLayer = 1 << POTENTIALLY_WALKABLE_LAYER;
            bool potentiallyWalkableHit = Physics.Raycast(ray, out hitInfo, maxRaycastDepth, potentiallyWalkableLayer);

            if (potentiallyWalkableHit)
            {
                Cursor.SetCursor(walkCursor, cursorHotspot, CursorMode.Auto);
                onMouseOverPotentiallyWalkable(hitInfo.point);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}