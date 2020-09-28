using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Handles pase and unpase and UI controlling
/// </summary>
public class GameProcess : MonoBehaviour
{
    private void Start()
    {
        LevelManager.OnGameOver += PauseGame;
    }

    public void PauseGame()
    {
        UI_Controller.SetActivePanel(UI_Controller.UI_Element.PauseGamePanel);
        Time.timeScale = 0;
    }

    public void UnPauseGame()
    {
        UI_Controller.SetActivePanel(UI_Controller.UI_Element.InGamePanel);
        Time.timeScale = 1;
    }
}
