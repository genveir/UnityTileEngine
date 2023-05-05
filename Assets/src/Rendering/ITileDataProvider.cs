using System.Collections.Generic;

namespace Assets.src.Rendering
{
    internal interface ITileDataProvider
    {
        IEnumerable<TileData> GetUpdatedTiles();
    }
}