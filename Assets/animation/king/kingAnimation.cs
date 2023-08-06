using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingAnimation : MonoBehaviour
{
    
    public Animator QueenAni;
    Action swingAction;
    // Start is called before the first frame update
    void Start()
    {
        
        QueenAni = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            PlayIdle();
        if (Input.GetKeyDown(KeyCode.S))
            PlaySwing(()=>{});
    }

    public void PlayIdle()
    {
        QueenAni.SetInteger("Status", 0);
    }

    public void PlaySwing(Action action)
    {
        QueenAni.SetInteger("Status", 1);
        swingAction = action;
    }
    public void CallSwingComplete(){
        swingAction();
    }

}
