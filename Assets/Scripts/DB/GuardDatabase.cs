using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Databases/GuardrDatabase", fileName = "GuardDatabase")]
public class GuardDataBase : BaseDB<GuardData>
{

}


/// <summary>
/// A model class of a guarding bot
/// </summary>
[System.Serializable]
public class GuardData
{
    [Tooltip("Material")]
    [SerializeField] private Material mainMaterial;
    public Material MainMaterial
    {
        get { return mainMaterial; }
        protected set { }
    }

    [Tooltip("Guard speed")]
    [SerializeField] private float speed;
    public float Speed
    {
        get { return speed; }
        protected set { }
    }

    [Tooltip("Guard delay between points")]
    [SerializeField] private float timeDelay;
    public float TimeDelay
    {
        get { return timeDelay; }
        protected set { }
    }


    [Tooltip("Guard points for moving number")]
    [SerializeField] private float pointsForMovingCount;
    public float PointsForMovingCount
    {
        get { return pointsForMovingCount; }
        protected set { }
    }

    [Tooltip("Guard list of Vector3 points for moving")]
    [SerializeField] private Vector3[] pointsForMoving;
    public Vector3[] PointsForMoving
    {
        get { return pointsForMoving; }
        set { pointsForMoving = value; }
    }

    [Tooltip("View radius")]
    [SerializeField] private float radius;
    public float Radius
    {
        get { return radius; }
        protected set { }
    }

    [Tooltip("View angle degrees")]
    [SerializeField] private float angle;
    public float Angle
    {
        get { return angle; }
        protected set { }
    }
}