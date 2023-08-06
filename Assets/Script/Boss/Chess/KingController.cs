using System;
using System.Collections;
using DG.Tweening;
using GameCore.Boss.ChessSpace;
using UnityEngine;

public class KingController: Chess {
    [SerializeField] float jumpPower;
    [SerializeField] float duration;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer[] swarldRenderer;
    [SerializeField] float destoryTime;
    [SerializeField] float spawnTime;
    [SerializeField] Collider2D[] collider2D;
    [SerializeField] float enabeledCollisionDistance; 
    [SerializeField] kingAnimation kingAnimation;
    public override void OnSpawn(Vector2 diraction)
    {
        if(GameObject.FindWithTag("Player")==null)
            Debug.Log("[BossAI]: could not find player tag");
        GameObject player =  GameObject.FindWithTag("Player");

        
        SpawnAnim(()=>{
            transform.DOJump(player.transform.position, jumpPower, 1, duration).OnComplete(()=>{
                OpenCollider();
                kingAnimation.PlaySwing(()=>{
                    CloseCollider();
                    onDelete();
                });
            });
        });
        
    }

    private void OpenCollider()
    {
        foreach(var collider in collider2D){
            collider.enabled = true;
        }
    }

    void onDelete(){
        foreach(var render in swarldRenderer){
            render.enabled = false;
        }
        DOTween.ToAlpha(()=>spriteRenderer.color,x=>spriteRenderer.color=x,0,destoryTime).OnComplete(()=>{
            Destroy(gameObject);
        });
    }
    private void CloseCollider()
    {
        foreach(var collider in collider2D){
            collider.enabled = false;
        }
    }
    void SpawnAnim(Action onComplete)
    {
        Color color = spriteRenderer.color;
        spriteRenderer.color = new Color(color.r, color.g, color.b, 0);
        DOTween.ToAlpha(() => spriteRenderer.color, x => spriteRenderer.color = x, 1, spawnTime).OnComplete(() =>
        {
            onComplete();
            
        });
    }
    public void TestSpawn(){
        OnSpawn(Vector2.zero);
    }
}