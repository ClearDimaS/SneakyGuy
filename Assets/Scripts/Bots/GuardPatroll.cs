using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardPatroll : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private FieldOfView fieldOfView;
    [SerializeField] private MeshRenderer mesh;
    private float speed;
    private float delayBeforNextMove;
    [SerializeField] private NavMeshAgent agent;
    private Vector3[] movePoints;

    private Vector3 currentTarget;
    private int currentTargetIndex = 0;
    bool isInit;
    public void Start()
    {
        LevelManager.OnGameOver += OnGameOver;
    }
    public void OnGameOver()
    {
        isInit = false;
    }

    /// <summary>
    /// Init data based on the one from LevelManager
    /// </summary>
    /// <param name="data"></param>
    // Start is called before the first frame update
    public void Init(GuardData data)
    {
        speed = data.Speed;
        delayBeforNextMove = data.TimeDelay;
        mesh.material = data.MainMaterial;
        movePoints = data.PointsForMoving;
        fieldOfView.viewAngle = data.Angle;
        fieldOfView.viewRadius = data.Radius;

        CancelInvoke();
        isInit = true;
        moved = false;
    }

    bool moved;
    private void Update()
    {
        animator.SetBool("walk", agent.hasPath);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    /// <summary>
    /// If a bot doesnt have a path give it a path and perform a delay based on its data
    /// </summary>
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isInit == true) 
        {
            if (agent.hasPath == false && moved == false)//agent.velocity.sqrMagnitude == 0 && moved == false)// && canChangeTarget)
            {
                Move();
                moved = true;
            }
        }
    }

    private void Move() 
    {
        Invoke("PerformDelay", delayBeforNextMove);
    }

    private void PerformDelay()
    {
        CancelInvoke();
        ChangeTarget();
        MoveToTargetPoint();
        moved = false;
    }

    /// <summary>
    /// Change target to the next in the list
    /// </summary>
    private void ChangeTarget() 
    {

        currentTargetIndex += 1;
        currentTargetIndex = currentTargetIndex % movePoints.Length;
        currentTarget = movePoints[currentTargetIndex];
    }

    /// <summary>
    ///  Start movement
    /// </summary>
    public void MoveToTargetPoint() 
    {
        agent.SetDestination(currentTarget);
        agent.stoppingDistance = 0.01f;
    }
}
