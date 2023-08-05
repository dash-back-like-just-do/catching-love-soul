using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingAnimation : MonoBehaviour
{
    private GameObject front;
    public Animator QueenAni;
    // Start is called before the first frame update
    void Start()
    {
        front = gameObject.transform.GetChild(1).gameObject;
        QueenAni = front.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            PlayFloat();
        if (Input.GetKeyDown(KeyCode.S))
            PlaySwing();
    }

    public void PlayFloat()
    {
        QueenAni.SetInteger("Status", 0);
    }

    public void PlaySwing()
    {
        QueenAni.SetInteger("Status", 1);
    }


}
