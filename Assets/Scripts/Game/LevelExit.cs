using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelExit : MonoBehaviour
{
    [SerializeField] private GameObject[] keysNeeded;
    private Dictionary<KeyObject, GameObject> ScriptObjDict;
    private BoxCollider boxCollider;
    private List<KeyObject> keyObjects;

    /// <summary>
    /// If there is no box collider add one
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        keyObjects = new List<KeyObject>();
        boxCollider = GetComponent<BoxCollider>();
        if (boxCollider == null)
            boxCollider = gameObject.AddComponent<BoxCollider>();
    }

    /// <summary>
    /// Level manager provides a list of data which is used for confirming when the door can be open
    /// </summary>
    /// <param name="_keyObjects"></param>
    public void Init(List<KeyObject> _keyObjects) 
    {
        keyObjects = _keyObjects;

        ResetItems();

        for (int i = 0; i < keyObjects.Count; i++) 
        {
            keysNeeded[i].transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, 0, 0);

            var mesh = keysNeeded[i].GetComponent<MeshRenderer>();
            if (mesh == null)
                mesh = keysNeeded[i].AddComponent<MeshRenderer>();

            mesh.material = keyObjects[i].data.MainMaterial;
            ScriptObjDict.Add(keyObjects[i], keysNeeded[i]);

            keysNeeded[i].SetActive(true);
        }
    }

    /// <summary>
    /// Just resetting values
    /// </summary>
    private void ResetItems()
    {
        ScriptObjDict = new Dictionary<KeyObject, GameObject>();

        foreach (GameObject obj in keysNeeded)
        {
            obj.SetActive(false);
        }
    }

    /// <summary>
    /// Try to get neseccary keys from a player's inventory, if all keys are taken win the hame
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        var rmvdItem = PlayerInventory.instance.RemoveFirst();
        while(rmvdItem != null) 
        {
            if (keyObjects.Contains(rmvdItem))
            {
                ScriptObjDict[rmvdItem].SetActive(false);
                keyObjects.Remove(rmvdItem);
            }
            rmvdItem = PlayerInventory.instance.RemoveFirst();
        }

        if(keyObjects.Count == 0)
            LevelManager.WinGame();
    }
}
