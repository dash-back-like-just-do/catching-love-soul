using UnityEngine;
namespace GameCore.Boss
{

    [RequireComponent(typeof(BossController))]
    public class BossAIController : MonoBehaviour {
        IBossController bossController;
        private void Start() {
            bossController = GetComponent<BossController>();
        }
    }
}