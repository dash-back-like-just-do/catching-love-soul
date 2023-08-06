using System.Collections;
using GameCore.Boss.ChessSpace;
using UnityEngine;
using DG.Tweening;
using System;

public class BishopController : Chess {
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] float speed;
    [SerializeField] float awakeTime;
    [SerializeField] float spawnTime;
    public override void OnSpawn(Vector2 diraction)
    {
        SpawnAnim(()=>{
            StartCoroutine(waitForMoving());
        });
        IEnumerator waitForMoving()
        {
            yield return new WaitForSeconds(awakeTime);
            rigidbody2D.velocity = diraction * speed;
            yield return new WaitForSeconds(10);
            Destroy(gameObject);
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
}
