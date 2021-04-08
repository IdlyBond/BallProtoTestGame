using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    private Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    public void SetEnabled(bool enabled)
    {
        _anim.Play(enabled ? "OnEnable" : "OnDisable");
    }
    

}
