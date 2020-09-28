
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] protected Joystick joystickMove;

    Vector3 player_Velocity;

    [Tooltip("A number betwenn 0 and 1. Move distance to activate")]
    [SerializeField] private float JoyStickMinSensitivity;
    
    /// <summary>
    /// A Vector with values between 0 and 1
    /// </summary>
    /// <param name="transform"></param>
    /// <returns></returns>
    public Vector3 InputPlayerMoveSpeed(Transform transform)
    {
        player_Velocity = new Vector3(0, 0, 0);
        if (joystickMove.Direction.magnitude > 0.0f)
        {
            transform.forward = GetRotation(transform.forward);
            player_Velocity = new Vector3(joystickMove.Horizontal, 0, joystickMove.Vertical);
        }

        return player_Velocity;
    }

    /// <summary>
    /// Change rotation of the played if he moves
    /// </summary>
    /// <param name="oldRotation"></param>
    /// <returns></returns>
    private Vector3 GetRotation(Vector3 oldRotation)
    {
        if (joystickMove.Direction.magnitude > 0.0f)
            return new Vector3(joystickMove.Direction.x, 0, joystickMove.Direction.y).normalized;
        else
            return oldRotation;
    }
}