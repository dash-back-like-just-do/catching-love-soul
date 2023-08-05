using GameCore.Basic;

namespace GameCore.Boss.core
{

    public class BossMoveState:BossState
    {
        public BossMoveState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            _bossContext.AnimationController.PlayFloat();
            _bossContext.DoMove(()=>{
                _stateMachine.MoveNextState();
            });
            
        }
        public override void OnLeave(){
            _bossContext.StopMove();
            base.OnLeave();
        }
        public override void OnUpdate(){
            
        }
    }
}