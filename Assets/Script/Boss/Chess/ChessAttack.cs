using ChessClub;
using GameCore.Boss.ChessSpace;
using UnityEngine;

public class ChessAttack : MonoBehaviour {
    [SerializeField] float damage;

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            DamagePlayer(other);
        }
        
    }
    private void DamagePlayer(Collision2D other)
    {
        IHpManager hpManager = GameObject.FindWithTag("GameManager").GetComponent<IHpManager>();
        hpManager.GetHpManager().Damage(other.gameObject, damage);
    }
    
}