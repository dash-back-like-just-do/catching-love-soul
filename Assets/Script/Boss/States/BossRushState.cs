using GameCore.Basic;

namespace GameCore.Boss.core
{

    public class BossRushState:BossState
    {
        public BossRushState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            
            _bossContext.DoMove(()=>{
                _stateMachine.MoveNextState();
            });
            
        }
        public override void OnLeave(){
            _bossContext.StopMove();
            base.OnLeave();
        }
        public override void OnUpdate(){
            if(_bossContext.DetactWall())
                _bossContext.StopMove();
        }
    }
}