using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace WarQuest.Characters
{
    public class PersistableSO : MonoBehaviour
    {
        [SerializeField]ArmourEquippedConfig armourEquippedConfig;
        [Header("Meta")]
         [SerializeField] string persistName;
        [Header("ScriptableObjects")]
         [SerializeField] List<ScriptableObject> objectsToPersist;

      //  void OnEnable()
        //{

        //    for (int i = 0; i < objectsToPersist.Count; i++)
          //  {
            //    if (File.Exists(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistName, i)))
            //    {
                //    BinaryFormatter binaryFormatter = new BinaryFormatter();
                //    FileStream file = File.Open(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistName, i), FileMode.Open);
                //    JsonUtility.FromJsonOverwrite((string)binaryFormatter.Deserialize(file), objectsToPersist[i]);
                 //   file.Close();
                //}
            //}
     //   }

      //  void OnDisable()
      //  {
            
            // will need to query armourequippedconfig to assign what is equipped
         //   for (int i = 0;i < objectsToPersist.Count; i++)
         //   {
              //  BinaryFormatter binaryFormatter = new BinaryFormatter();
             //   FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.pso", persistName, i));
              //  var json = JsonUtility.ToJson(objectsToPersist[1]);
               // binaryFormatter.Serialize(file, json);
              //  file.Close();
          //  }

      //  }
    }
}
