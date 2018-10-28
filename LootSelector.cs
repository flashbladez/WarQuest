using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace WarQuest.Characters
{
    public class LootSelector : MonoBehaviour
    {
        [SerializeField] GameObject[] lootToSelectDrop;
        int randomItem;
        GameObject lootSelected;

        void Start()
        {
            randomItem = UnityEngine.Random.Range(0, lootToSelectDrop.Length);
            InstantiateItem();
        }

        void InstantiateItem()
        {
            lootSelected = Instantiate(lootToSelectDrop[randomItem], gameObject.transform);
            lootSelected.transform.SetParent(transform);
            lootSelected.transform.localPosition = Vector3.zero;
        }
    }
}
