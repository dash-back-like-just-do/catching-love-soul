using System;
using UnityEngine;
namespace GameCore.Boss.ChessSpace
{
    public abstract class Chess:MonoBehaviour
    {
        public abstract void OnSpawn(Vector2 diraction);
    }
}