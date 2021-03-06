﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Swaps betwen panels, nothing more
/// </summary>
public class UI_Controller : MonoBehaviour
{
    public enum UI_Element
    {
        InGamePanel,
        PauseGamePanel,
        LoseGamePanel,
        WinGamePanel,
    }


    public static UI_Controller Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        SetActivePanel(0);
    }


    public void HideAllPanels()
    {
        foreach (var go in HideInEditor)
        {
            go.SetActive(false);
        }
    }


    // Panels
    public Dictionary<UI_Element, GameObject> Panels
    {
        get
        {
            if (panels != null)
            {
                return panels;
            }
            else
            {
                panels = new Dictionary<UI_Element, GameObject>();
                FindAndAddAllPanels();

                return panels;
            }
        }
    }

    // Additionals

    public List<GameObject> HideInEditor = new List<GameObject>();

    private Dictionary<UI_Element, GameObject> panels;

    public static void SetActivePanel(UI_Element element)
    {
        foreach (var panel in Instance.Panels.Values)
        {
            if (panel) panel.SetActive(false);
        }

        if (Instance.Panels[element])
            Instance.Panels[element].SetActive(true);
    }

    public void SetActivePanel(int element)
    {
        var elementAsEnum = (UI_Element)element;

        SetActivePanel(elementAsEnum);
    }

    private void FindAndAddAllPanels()
    {
        panels.Clear();
        panels.Add(UI_Element.InGamePanel, Instance.transform.Find("InGamePanel").gameObject);
        panels.Add(UI_Element.PauseGamePanel, Instance.transform.Find("PauseGamePanel").gameObject);
        panels.Add(UI_Element.LoseGamePanel, Instance.transform.Find("LoseGamePanel").gameObject);
        panels.Add(UI_Element.WinGamePanel, Instance.transform.Find("WinGamePanel").gameObject);

    }
}