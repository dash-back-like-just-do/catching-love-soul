using System;
using GameCore.Basic;
namespace GameCore.Boss.core
{
    public class BossState:IState
    {
        protected BossController _bossContext;
        protected IStateMachineContext _stateMachine;
        protected Action _onComplete; 
        public BossState(BossController bossContext,IStateMachineContext stateMachine){
            this._bossContext = bossContext;
            this._stateMachine = stateMachine;
        }
        public virtual void OnEnter(){

        }
        public virtual void OnLeave(){
            if(_onComplete!=null)
                _onComplete();
            _onComplete = null;
        }
        public virtual void OnUpdate(){

        }
        public virtual void OnFixUpdate(){

        }
        public virtual void CallOnLeave(Action func){
            _onComplete = func;
        }
    }
    public class BossStateTag{
        public static string Move = "Move",
                            Idle = "Idle",
                            Attack = "Attack",
                            Hurt = "Hurt",
                            Rush = "Rush";

    }
}