using Assets.src.IO.Models;
using UnityEngine;

namespace Assets.src.IO
{
    internal class HttpInputHandler : IInputHandler
    {
        private readonly HttpWorldEngineClient _httpWorldEngineClient = new();

        public void Move(Vector2Int movement)
        {
            var command = new CommandDto()
            {
                OrderType = CommandTypes.Move,
                Parameters = new[] { movement.x, movement.y }
            };

            _httpWorldEngineClient.SendCommand(command);
        }
    }
}