using System.Collections.Generic;
using System;
using UnityEngine;
namespace GameCore.Basic
{
    public class StateMachine<T> : IStateMachineContext where T : Enum 
    {
        Dictionary<T,IState> _stateMap;
        T _currentState;
        T _defaultState;
        T _nextState;
        public IState CurrentState => _stateMap[_currentState];
        public StateMachine(){
            _stateMap = new Dictionary<T, IState>();
        }
        public void SetState(T state){
            _currentState = state;
        }
        public void SetNextState(T state){
            _nextState = state;
        }
        public void UnSetNextState(){
            _nextState = _defaultState;
        }
        public void SetDefaultState(T state){
            _defaultState = state;
        }
        public void Start(){
            _currentState ??= _defaultState;
            CurrentState.OnEnter();
        }
        public void ChangeState(T state,Action onComplete = null,bool hardInsertState = false){
            IState newState = _stateMap[state];
            if(_currentState.Equals(state))
                return;
            IState lastState = CurrentState;
            CurrentState.OnLeave();
            newState.CallOnLeave(onComplete);
            _currentState = state;
            newState.OnEnter();
            if(!hardInsertState)
                lastState.OnComplete();
            Debug.Log($"ChangeTo:{state}");
        }
        public void AddState(T stateName,IState state){
            _stateMap.Add(stateName,state);
        }
        
        public void OnUpdate(){
            CurrentState.OnUpdate();
        }
        public void OnFixUpdate(){
            CurrentState.OnFixUpdate();
        }
        public void MoveNextState(){
            IState lastState = CurrentState;
            if(!_nextState.Equals(_defaultState)){
                _currentState = _nextState;
                UnSetNextState();
            }
            else
                _currentState = _defaultState;
            lastState.OnLeave();
            CurrentState.OnEnter();
            Debug.Log($"ChangeTo:{GetCurrentState()}");
            lastState.OnComplete();
        }
        public T GetCurrentState(){
            return _currentState;
        }
        
    }
    public interface IState{
        void OnEnter();
        void OnLeave();
        void OnUpdate();
        void OnFixUpdate();
        void CallOnLeave(Action func);
        void OnComplete();
    }
    public interface IStateMachineContext{
        void MoveNextState();
    }
    
}