using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Separating view from logic (trying)
/// </summary>
public class UI_ButtonsView : MonoBehaviour
{
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameProcess gameProcess;

    public void PauseLevel()
    {
        //UI_Controller.SetActivePanel(UI_Controller.UI_Element.PauseGamePanel);
        gameProcess.PauseGame();
    }

    public void ContinueLevel()
    {
        gameProcess.UnPauseGame();
    }

    public void NextLevel()
    {
        gameProcess.UnPauseGame();
        levelManager.LoadLevel(1);
    }

    public void RestartLevel()
    {
        gameProcess.UnPauseGame();
        levelManager.LoadLevel();
    }
}
