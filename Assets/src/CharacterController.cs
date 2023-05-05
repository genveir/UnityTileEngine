using Assets.src.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.src
{
    internal class CharacterController : MonoBehaviour
    {
        private readonly IInputHandler _inputHandler;

        private Vector2Int _movement;

        public CharacterController()
        {
            _inputHandler = new HttpInputHandler();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            _movement = new Vector2Int(
                value.x == 0 ? 0 : (value.x < 0 ? -1 : 1),
                value.y == 0 ? 0 : (value.y < 0 ? -1 : 1)
            );
        }

        public void Update()
        {
            if (_movement != Vector2Int.zero)
            {
                _inputHandler.Move(_movement);

                _movement = Vector2Int.zero;
            }
        }
    }
}