using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/KeysDatabase", fileName = "KeysDatabase")]
public class KeysDataBase : BaseDB<KeysData>
{

}


/// <summary>
/// A model class of a key which is needed to open a door
/// </summary>
[System.Serializable]
public class KeysData
{
    [Tooltip("Material")]
    [SerializeField] private Material mainMaterial;
    public Material MainMaterial
    {
        get { return mainMaterial; }
        protected set { }
    }

    [Tooltip("Time for using")]
    [SerializeField] private float useTime;
    public float UseTime
    {
        get { return useTime; }
        protected set { }
    }
}