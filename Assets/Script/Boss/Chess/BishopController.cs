using GameCore.Boss.ChessSpace;
using UnityEngine;

public class BishopController : Chess {
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] float speed;

    

    public override void OnSpawn(Vector2 diraction)
    {
        rigidbody2D.velocity = diraction*speed;
    }
}
