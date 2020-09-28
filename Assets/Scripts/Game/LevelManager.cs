using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelExit levelExit;
    [SerializeField] private float y_coord_For_Spawn;
    [SerializeField] private Transform LeftBottomCorner;
    [SerializeField] private Transform RightTopCorner;
    [SerializeField] private int minEnemyNumber = 2;
    [SerializeField] private int maxEnemyNumber = 5;

    [SerializeField] private GameObject PlayerObject;

    [SerializeField] private GuardDataBase guardDataBase;
    [SerializeField] private GameObject GuardsPrefab;
    private List<GuardPatroll> GuardsSpawned;
    public static Dictionary<GuardPatroll, GameObject> Guards;

    [SerializeField] private KeysDataBase keysDataBase;
    [SerializeField] private GameObject KeysPrefab;
    private List<KeyObject> KeysSpawned;
    public static Dictionary<KeyObject, GameObject> Keys;

    private int LevelNumber = 0;
    static bool isLost;
    private System.Random rand;

    private int enemyCount;
    private int keysNumber;

    public static Action OnGameOver;

    private void Start()
    {
        Guards = new Dictionary<GuardPatroll, GameObject>();
        GuardsSpawned = new List<GuardPatroll>();

        Keys = new Dictionary<KeyObject, GameObject>();
        KeysSpawned = new List<KeyObject>();

        LoadLevel();
    }

    public static void LoseGame()
    {
        if (isLost == false) 
        {
            OnGameOver?.Invoke();
            UI_Controller.SetActivePanel(UI_Controller.UI_Element.LoseGamePanel);
        }
        isLost = true;
    }

    public static void WinGame()
    {
        OnGameOver?.Invoke();
        UI_Controller.SetActivePanel(UI_Controller.UI_Element.WinGamePanel);
    }

    public void LoadLevel(int AddToCurrentLevelNumber = 0)
    {
        isLost = false;
        RestAllObjects();
        //UI_Controller.SetActivePanel(UI_Controller.UI_Element.InGamePanel);
        LevelNumber += AddToCurrentLevelNumber;
        rand = new System.Random(LevelNumber);

        SpawnKeys();
        SpawnEnemies();
        levelExit.Init(KeysSpawned);
        Invoke("SpawnPlayer", 1.0f);
    }

    #region Spawning objects
    /// <summary>
    /// Deactivate all the obejcts. This is done not to use Instantiate and destroy which decrease perfomance. A kind of a Pool.
    /// </summary>
    private void RestAllObjects()
    {
        PlayerObject.SetActive(false);

        foreach (KeyValuePair<GuardPatroll, GameObject> keyValuePair in Guards)
        {
            keyValuePair.Value.SetActive(false);
        }

        foreach (KeyValuePair<KeyObject, GameObject> keyValuePair in Keys)
        {
            keyValuePair.Value.SetActive(false);
        }
    }

    /// <summary>
    /// Spawn the keys and send data to their classes to Initialize them
    /// </summary>
    private void SpawnKeys()
    {
        keysDataBase.ResetCurrentIndex();

        keysNumber = rand.Next(1, Mathf.Min(keysDataBase.GetLength() + 1, 4));
        Debug.Log(keysNumber);
        for (int i = 0; i < keysNumber; i++)
        {
            if (i >= KeysSpawned.Count)
            {
                var obj = Instantiate(KeysPrefab, GetRandomPositionOnNavMesh(), Quaternion.Euler(90, 0 , 0));
                KeysSpawned.Add(obj.GetComponentInChildren<KeyObject>());
                Keys.Add(KeysSpawned[i], obj);
            }
            else
            { 
                Keys[KeysSpawned[i]].SetActive(true);
                Keys[KeysSpawned[i]].transform.position = GetRandomPositionOnNavMesh();
                Keys[KeysSpawned[i]].transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, 0, 0);
            }
            Keys[KeysSpawned[i]].SetActive(true);
            KeysData keyData;
            if (i == 0)
            {
                keyData = keysDataBase.GetPrev();
            }
            else 
            {
                keyData = keysDataBase.GetNext();
            }
                
            KeysSpawned[i].Init(keyData);
        }
    }
    
    /// <summary>
    /// Place a player to the start position and activate him
    /// </summary>
    private void SpawnPlayer()
    {
        PlayerObject.transform.position = LeftBottomCorner.transform.position;
        PlayerObject.SetActive(true);
    }

    /// <summary>
    /// Same as SpawnKeys
    /// </summary>
    private void SpawnEnemies()
    {
        enemyCount = rand.Next(minEnemyNumber, maxEnemyNumber);
        //keysNumber = rand.Next();
        for (int i = 0; i < enemyCount; i++)
        {
            if (i >= Guards.Count)
            {
                var obj = Instantiate(GuardsPrefab, GetRandomPositionOnNavMesh(), Quaternion.identity);
                GuardsSpawned.Add(obj.GetComponentInChildren<GuardPatroll>());
                Guards.Add(GuardsSpawned[i], obj);
            }
            else
            {
                Guards[GuardsSpawned[i]].SetActive(true);
                Guards[GuardsSpawned[i]].transform.position = GetRandomPositionOnNavMesh();
                Guards[GuardsSpawned[i]].transform.rotation = Quaternion.identity;
            }
            GuardData guardData = guardDataBase.GetRandomElement(rand);
            guardData.PointsForMoving = GetRandomPointsForMoving(guardData);
            GuardsSpawned[i].Init(guardData);
        }
    }

    #endregion

    #region Helper functions for geting random positions for spawn
    /// <summary>
    /// Get random points between which bots will move
    /// </summary>
    /// <param name="guardData"></param>
    /// <returns></returns>
    private Vector3[] GetRandomPointsForMoving(GuardData guardData)
    {
        List<Vector3> tempVal = new List<Vector3>();
        for (int i = 0; i < guardData.PointsForMovingCount; i++)
        {
            tempVal.Add(GetRandomPositionOnNavMesh());
        }
        Vector3[] retVal = tempVal.ToArray();
        return retVal;
    }

    /// <summary>
    /// Get random position which should be on a NavMesh
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPositionOnNavMesh()
    {
        Vector3 retVal = GetRandomPositionInsideBounds();
        while(true)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(retVal, out hit, 0.3f, NavMesh.AllAreas))
            {
                break;
            }
            retVal = GetRandomPositionInsideBounds();
        }

        return retVal;
    }

    /// <summary>
    /// Get random position in a square which is defined with 2 points
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPositionInsideBounds()
    {
        return new Vector3(rand.Next((int)LeftBottomCorner.position.x, (int)RightTopCorner.position.x), y_coord_For_Spawn, rand.Next((int)LeftBottomCorner.position.z, (int)RightTopCorner.position.z));
    }
    #endregion
}
