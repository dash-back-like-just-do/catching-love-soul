
using GameCore.Basic;

namespace GameCore.Boss.core
{
    public class BossHurtState:BossState
    {
        public BossHurtState(BossController bossContext,IStateMachineContext stateMachine) : base(bossContext,stateMachine){
            
        }
        public override void OnEnter(){
            _bossContext.AnimationController.PlayHurt();
        }

        public override void OnUpdate(){

        }
    }
}