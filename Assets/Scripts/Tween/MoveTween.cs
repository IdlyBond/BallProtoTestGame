using System;
using UnityEngine;

public class MoveTween : MonoBehaviour
{
    public bool saveStartPos = true;
    private Vector3 startPos;
    public Vector3 to;
    public float time;
    public float delay;

    private void Awake()
    {
        startPos = transform.position;
    }

    void OnEnable()
    {
        if (saveStartPos) transform.position = startPos;
        LeanTween.move(GetComponent<RectTransform>(), to, time).setDelay(delay);
    }
}
