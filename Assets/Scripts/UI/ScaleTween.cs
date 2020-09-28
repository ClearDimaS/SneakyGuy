using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// Scales UI elements
/// </summary>
public class ScaleTween : MonoBehaviour
{
    [SerializeField] bool OneByOne = true;

    [SerializeField] float delayTime = 0.4f;


    private void OnEnable()
    {
        LeanTween.scale(gameObject, new Vector3(0, 0, 0), 0.0f).setIgnoreTimeScale(true);
        if (OneByOne)
            ScaleUp();
        else
            ScaleUpDelayed();

    }
    public void ScaleUp()
    {
        LeanTween.scale(gameObject, new Vector3(1, 1, 1), (transform.GetSiblingIndex() + 1) * 0.2f + 0.2f).setIgnoreTimeScale(true);
    }

    public void ScaleUpDelayed()
    {
        Invoke("ScaleUp", transform.GetSiblingIndex() * delayTime);
    }
}