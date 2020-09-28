using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Debug.Log("More than 1 instance of an inventory found so sorry bro");
    }

    private void Start()
    {
        LevelManager.OnGameOver += resetItems;
    }

    /// <summary>
    /// Clear inventory
    /// </summary>
    private void resetItems()
    {
        items.Clear();
    }

    private List<KeyObject> items = new List<KeyObject>();

    public bool Add(KeyObject item)
    {
        items.Add(item);

        onItemChangedCallback();

        return true;
    }

    public void Remove(KeyObject item)
    {
        items.Remove(item);

        onItemChangedCallback();
    }

    /// <summary>
    /// Just remove the first value, null if nothing to remove
    /// </summary>
    /// <returns></returns>
    public KeyObject RemoveFirst()
    {
        KeyObject retVal = null;
        if (items.Count > 0)
        {
            retVal = items[0];
            items.RemoveAt(0);

            onItemChangedCallback();
        }
        return retVal;
    }

    /// <summary>
    /// Might be useful in the future
    /// </summary>
    public void onItemChangedCallback()
    {

    }
}
