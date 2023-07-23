using System.Collections.Generic;
using System;

namespace GameCore.Basic
{
    public class StateMachine
    {
        Dictionary<string,IState> _stateMap;
        IState _currentState;
        public StateMachine(){
            _stateMap = new Dictionary<string, IState>();
        }
        public void ChangeState(string state,Action onComplete = null){
            IState newState = _stateMap[state];
            if(_currentState.Equals(_stateMap[state]))
                return;
            _currentState.OnLeave();
            _currentState = newState;
            newState.OnEnter();
            newState.CallOnLeave(onComplete);
        }
        public void AddState(string stateName,IState state){
            _stateMap.Add(stateName,state);
        }
        
        public void OnUpdate(){
            _currentState.OnUpdate();
        }
        public void OnFixUpdate(){
            _currentState.OnFixUpdate();
        }
    }
    public interface IState{
        void OnEnter();
        void OnLeave();
        void OnUpdate();
        void OnFixUpdate();
        void CallOnLeave(Action func);
    }
}