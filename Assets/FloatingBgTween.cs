using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingBgTween : MonoBehaviour
{
    public Vector3 pos1;
    public Vector3 pos2;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        MoveForward();
    }

    private void MoveForward()
    {
        LeanTween.move(gameObject, pos2, 7f).setOnComplete(MoveBack);

    }
    
    private void MoveBack()
    {
        LeanTween.move(gameObject, pos1, 7f).setOnComplete(MoveForward);

    }

}
