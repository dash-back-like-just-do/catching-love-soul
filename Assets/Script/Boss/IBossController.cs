
using UnityEngine;
using System;

namespace GameCore.Boss
{
    public interface IBossController
    {
        void OnIdle();
        void OnAttack(int attackId, Action onComplete);
        void OnMove(Vector2 dir, Action onComplete, float sec = 0);
        void OnRush(Vector2 dir, Action onComplete, float sec = 0);
        void TurnAround(bool faceToLeft);
    }
}