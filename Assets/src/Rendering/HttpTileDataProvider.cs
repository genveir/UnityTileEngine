using Assets.src.IO;
using Assets.src.IO.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.src.Rendering
{
    internal class HttpTileDataProvider : ITileDataProvider
    {
        private readonly HttpWorldEngineClient _worldEngineClient = new();

        public IEnumerable<TileData> GetUpdatedTiles()
        {
            Debug.Log("getting tiles");

            var dtos = _worldEngineClient.GetUpdatedTiles();

            return dtos.Select(Map);
        }

        private TileData Map(TileDto dto)
        {
            return new()
            {
                X = dto.X,
                Y = dto.Y,
                Floor = dto.Floor,
                Creature = dto.Creature == 0 ? null : dto.Creature,
                Item = dto.Item == 0 ? null : dto.Item,
                Effect = dto.Effect == 0 ? null : dto.Effect,
                IsCharacterLocation = dto.IsCharacterLocation
            };
        }
    }
}