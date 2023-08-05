using System.Collections;
using GameCore.Boss.ChessSpace;
using UnityEngine;
using DG.Tweening;
public class PawnController : Chess {
    [SerializeField] float jumpPower;
    [SerializeField] float duration;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float destoryTime;
    [SerializeField] float awakeTime;

    public override void OnSpawn(Vector2 diraction)
    {
        GameObject player =  GameObject.FindWithTag("Player");
        StartCoroutine(waitForMoving());
        IEnumerator waitForMoving(){
            yield return new WaitForSeconds(awakeTime);
            transform.DOJump(player.transform.position,jumpPower,1,duration).OnComplete(OnComplete);
        }
    }
    void OnComplete(){
        DOTween.ToAlpha(()=>spriteRenderer.color,x=>spriteRenderer.color=x,0,destoryTime).OnComplete(()=>{
            Destroy(gameObject);
        });
    }
    
    public void TestSpawn(){
        OnSpawn(Vector2.zero);
    }
}
