using Assets.src.IO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using UnityEngine;

namespace Assets.src.IO
{
    internal class HttpWorldEngineClient
    {
        private readonly HttpClient _httpClient;

        public HttpWorldEngineClient()
        {
            _httpClient = new()
            {
                BaseAddress = new Uri("https://localhost:7233/")
            };
        }

        public IEnumerable<TileDto> GetUpdatedTiles()
        {
            Debug.Log("querying updated tiles from service");

            var response = _httpClient.GetAsync("world/tiles/updated").Result;

            IEnumerable<TileDto> result = new List<TileDto>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var json = response.Content.ReadAsStringAsync().Result;
                var tiles = JsonUtility.FromJson<TilesDto>(json);

                result = Decode(Convert.FromBase64String(tiles.tiles));

                Debug.Log($"received {result.Count()} tiles from service");
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                Debug.Log($"receved no tiles from service");
            }
            else
            {
                Debug.Log("Failed to update");
            }

            return result;
        }

        [Serializable]
        public class TilesDto
        {
            public string tiles;
        }

        public void SendCommand(CommandDto command)
        {
            var bytes = command.ToByteArray();
            var base64 = Convert.ToBase64String(bytes);
            var content = new CmdDto { command = base64 };
            var json = JsonUtility.ToJson(content);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = _httpClient.PostAsync("world/player/command", httpContent).Result;

            if (!response.IsSuccessStatusCode)
            {
                Debug.Log("Failed to send command");
            }
        }

        [Serializable]
        public class CmdDto
        {
            public string command;
        }

        private IEnumerable<TileDto> Decode(byte[] tiles)
        {
            var num = BitConverter.ToInt32(tiles, 0);

            List<TileDto> result = new();
            for (int n = 0; n < num; n++)
            {
                result.Add(TileDto.FromByteArray(tiles, 4 + 25 * n));
            }
            return result;
        }
    }
}