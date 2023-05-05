using UnityEngine;

namespace Assets.src.IO
{
    internal interface IInputHandler
    {
        void Move(Vector2Int movement);
    }
}