using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    private GameObject front;
    public Animator PlayerAni;
    public Animator bloods;
    // Start is called before the first frame update
    void Start()
    {
        front = gameObject.transform.GetChild(0).gameObject;
        PlayerAni = front.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (Input.GetKey(KeyCode.Alpha1))
            PlayWalk();
        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayAttack();
        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayRoll();
        if (Input.GetKeyDown(KeyCode.Alpha4))
            PlayDead();
        if (Input.GetKeyUp(KeyCode.Alpha1))
            PlayIdle();
        if (Input.GetKeyUp(KeyCode.Alpha2))
            PlayIdle();
        if (Input.GetKeyUp(KeyCode.Alpha3))
            PlayIdle();
    }

    public void PlayIdle()
    {
        PlayerAni.SetInteger("Status", 0);
    }

    public void PlayWalk()
    {
        PlayerAni.SetInteger("Status", 1);
    }

    public void PlayAttack()
    {
        PlayerAni.SetInteger("Status", 2);
    }

    public void PlayRoll()
    {
        PlayerAni.SetInteger("Status", 3);
    }

    public void PlayDead()
    {
        PlayerAni.SetInteger("Status", 4);
        bloods.SetInteger("Status", 1);
    }

    public void PlayHurt()
    {
        bloods.SetInteger("Status", 1);
    }

}
