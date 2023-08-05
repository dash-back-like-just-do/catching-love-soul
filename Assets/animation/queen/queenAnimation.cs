using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenAnimation : MonoBehaviour
{
    private GameObject front;
    public Animator QueenAni;
    // Start is called before the first frame update
    void Start()
    {
        front = gameObject.transform.GetChild(0).gameObject;
        QueenAni = front.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayFloat()
    {
        QueenAni.SetInteger("Status", 0);
    }

    public void PlaySummon()
    {
        QueenAni.SetInteger("Status", 1);
    }

    public void PlayRush()
    {
        QueenAni.SetInteger("Status", 2);
    }


}
