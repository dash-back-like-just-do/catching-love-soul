using System.Collections;
using GameCore.Boss.ChessSpace;
using UnityEngine;

public class BishopController : Chess {
    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] float speed;
    [SerializeField] float awakeTime;
    

    public override void OnSpawn(Vector2 diraction)
    {
        StartCoroutine(waitForMoving());
        IEnumerator waitForMoving(){
            yield return new WaitForSeconds(awakeTime);
            rigidbody2D.velocity = diraction*speed;
            yield return new WaitForSeconds(10);
            Destroy(gameObject);
        }
    }
}
