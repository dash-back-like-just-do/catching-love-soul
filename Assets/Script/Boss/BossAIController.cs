using System.Collections;
using GameCore.Boss.Combatent;
using UnityEngine;
using utils;

namespace GameCore.Boss
{

    [RequireComponent(typeof(BossController))]
    public class BossAIController : MonoBehaviour {
        
        IBossController bossController;
        [SerializeField] float awakeCount = 2;
        [SerializeField] int currentState = 0;
        [SerializeField] float[] changeStateHP;
        [SerializeField] BossCombatent bossCombatent;
        Transform player;
        private void Start() {
            bossController = GetComponent<BossController>();
            if(GameObject.FindWithTag("Player")==null)
                Debug.Log("[BossAI]: could not find player tag");
            player = GameObject.FindWithTag("Player").transform;
            StartCoroutine(waitForAwake());

            IEnumerator waitForAwake(){
                yield return new WaitForSeconds(awakeCount);
                currentState = 1;
                Debug.Log("On BOSS start");
                MakeDecision();
            }
        }
        private void Update() {
            if(currentState>0 && currentState<=3){
                float currentHP = bossCombatent.GetHp();
                if( currentHP<=changeStateHP[currentState-1])
                    currentState ++;
            }
        }
        private void FixedUpdate() {
            
            
        }
        
        public void MakeDecision(){
            if(currentState>=1){
                if(closeToPlayer()){
                    LookatPlayer();
                    bossController.OnAttack(0,()=>{
                        MoveToPlayer(()=>{
                            if(currentState==3){
                                bossController.OnAttack(2,()=>{
                                    //back to idle
                                    RestartDecision();
                                });
                            }
                            else{
                                //back to idle
                                RestartDecision();
                            }
                        });
                    });
                }
                else
                {
                    if (currentState >= 4)
                    {
                        bossController.OnAttack(3, () =>
                        {
                            MoveRandomDision();
                        });
                    }
                    else{
                        MoveRandomDision();
                    }
                }
            }
        }

        private void MoveRandomDision()
        {
            MoveRandomly(() =>
            {
                if (currentState >= 2)
                {
                    bossController.OnAttack(1, () =>
                    {
                        MoveAwayFromPlayer(() =>
                        {
                            RushToPlayer(() =>
                            {
                                //back to idle
                                RestartDecision();
                            });
                        });
                    });
                }
                else
                {
                    RushToPlayer(() =>
                    {
                        //back to idle
                        RestartDecision();
                    });
                }
            });
        }

        [SerializeField] float restartDecisionTime = 3;
        void RestartDecision(){
            bossController.OnIdle();
            StartCoroutine(waitForNextLoop());
            IEnumerator waitForNextLoop(){
                yield return new WaitForSeconds(restartDecisionTime);
                Debug.Log("restart");
                MakeDecision();
            }
        }
        [SerializeField] float minDistanceToPlayer =10f;
        bool closeToPlayer(){
            Vector2 playerPos = player.position;
            Vector2 bossPos = transform.position;
            
            return Vector2.Distance(playerPos,bossPos) < minDistanceToPlayer;
        }
        
        void MoveAwayFromPlayer(System.Action onComplete){
            Vector2 playerPos = player.position;
            Vector2 bossPos = transform.position;
            
            bossController.OnMove( 
                (- new Vector2(playerPos.x-bossPos.x,playerPos.y-bossPos.y)).normalized,
                onComplete,
                Vector2.Distance(playerPos,bossPos)*moveSecScale);
        }
        void LookatPlayer(){
            Vector2 playerPos = player.position;
            Vector2 bossPos = transform.position;
            Vector2 direction =new Vector2(playerPos.x-bossPos.x,playerPos.y-bossPos.y).normalized;
            bossController.TurnAround(direction.x>0);
        }
        [Header("Move")]
        [SerializeField] float moveSecScale = .5f;
        [SerializeField] float maximaMoveDuration = 3;
        void MoveToPlayer(System.Action onComplete){
            Vector2 playerPos = player.position;
            Vector2 bossPos = transform.position;
            
            bossController.OnMove( 
                new Vector2(playerPos.x-bossPos.x,playerPos.y-bossPos.y).normalized,
                onComplete,
                Mathf.Min(maximaMoveDuration,Vector2.Distance(playerPos,bossPos)*moveSecScale));
        }
        [Header("MoveRandomly")]
        [SerializeField] Vector2 maxRandomMoveDir = new Vector2(20,20);
        [SerializeField] Vector2 minRandomMoveDir = new Vector2(15,15);
        [SerializeField] float maxRandomMoveTime = 5;
        [SerializeField] float minRandomMoveTime = 2;
        void MoveRandomly(System.Action onComplete){
            
            bossController.OnMove(
                new Vector2(Random.Range(minRandomMoveDir.x,maxRandomMoveDir.x),Random.Range(minRandomMoveDir.y,maxRandomMoveDir.y)).normalized,
                onComplete,
                Random.Range(minRandomMoveTime,maxRandomMoveTime));
        }   
        [Header("RushToPlayer")]
        [SerializeField] float rushScale = 4f;
        [SerializeField] float rushSec = 2;
        void RushToPlayer(System.Action onComplete){
            Vector2 playerPos = player.position;
            Vector2 bossPos = transform.position;
            
            bossController.OnRush( 
                new Vector2(playerPos.x-bossPos.x,playerPos.y-bossPos.y).normalized * rushScale,
                onComplete,
                rushSec);
        }
    }
}