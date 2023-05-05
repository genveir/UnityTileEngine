using Assets.src.Rendering;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileEngine : MonoBehaviour
{
    public Tilemap TilemapFloor;
    public Tilemap TilemapCreatures;
    public Tilemap TilemapEffects;

    public GameObject Character;

    private Sprite[] _sprites;

    private readonly ITileDataProvider _tileDataProvider;

    public TileEngine() : base()
    {
        _tileDataProvider = new HttpTileDataProvider();
    }

    // Start is called before the first frame update
    private void Start()
    {
        _sprites = Resources.LoadAll<Sprite>("Tiles");
        Debug.Log($"Loaded {_sprites.Length} sprites");

        SetMap();
    }

    private Tile BuildTile(Sprite sprite)
    {
        var instance = ScriptableObject.CreateInstance<Tile>();
        instance.sprite = sprite;

        return instance;
    }

    private void Update()
    {
        SetMap();
    }

    private void SetMap()
    {
        var tileData = _tileDataProvider.GetUpdatedTiles();

        foreach (var td in tileData)
        {
            var vector = new Vector3Int(td.X, td.Y);
            SetTile(TilemapFloor, vector, td.Floor);
            SetTile(TilemapCreatures, vector, td.Creature);
            SetTile(TilemapEffects, vector, td.Effect);

            if (td.IsCharacterLocation)
            {
                Character.transform.position = TilemapFloor.GetCellCenterWorld(vector);
            }
        }
    }

    private void SetTile(Tilemap map, Vector3Int vector, int? spriteNum)
    {
        Tile tile;
        if (spriteNum == null)
        {
            tile = null;
        }
        else
        {
            var sprite = _sprites[spriteNum.Value];
            tile = BuildTile(sprite);
        }

        map.SetTile(vector, tile);
    }
}