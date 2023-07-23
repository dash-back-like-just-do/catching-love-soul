using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameCore.Boss.core
{

    public class BossMoveState:BossState
    {
        public BossMoveState(BossController bossContext) : base(bossContext){
            
        }
        public override void OnEnter(){

        }
        public override void OnLeave(){
            _bossContext.StopMove();
            base.OnLeave();
        }
        public override void OnUpdate(){
            _bossContext.DoMove();
        }
    }
}