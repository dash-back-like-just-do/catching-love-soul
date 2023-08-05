using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenAnimation : MonoBehaviour
{
    public Animator QueenAni;
    System.Action summondone;
    // Start is called before the first frame update
    void Start()
    {
        QueenAni = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
            PlayFloat();
        
        if (Input.GetKeyDown(KeyCode.E))
            PlayRush();

    }

    public void PlayFloat()
    {
        QueenAni.SetInteger("Status", 0);
    }

    public void PlaySummon(System.Action onComplete)
    {
        summondone = onComplete;
        QueenAni.SetInteger("Status", 1);
    }

    public void PlayRush()
    {
        QueenAni.SetInteger("Status", 2);
    }
    
    public void SummonDone()
    {
        summondone();
    }

}
