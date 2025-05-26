using System.Collections.Generic;
using UnityEngine;

namespace Hexagons
{
    public class HexGrid : MonoBehaviour
    {
        private readonly Dictionary<Vector3Int, Hex> _hexTiles = new();
        private readonly Dictionary<Vector3Int, List<Vector3Int>> _hexNeighbors = new();

        private void Start()
        {
            foreach (var hex in FindObjectsByType<Hex>(FindObjectsSortMode.None))
            {
                _hexTiles[hex.hexCoords] = hex;
            }
        }

        public Hex GetTile(Vector3Int coords)
        {
            _hexTiles.TryGetValue(coords, out var result);
            return result;
        }

        public List<Vector3Int> GetNeighbors(Vector3Int coords)
        {
            if (_hexTiles.ContainsKey(coords) == false)
                return new List<Vector3Int>();
            
            if (_hexNeighbors.TryGetValue(coords, out var neighbors))
                return neighbors;
            
            _hexNeighbors.Add(coords, new List<Vector3Int>());

            foreach (var d in HexCoords.Directions)
            {
                var direction = new Vector3Int(d.Q, d.R, d.S);
                if (_hexTiles.ContainsKey(coords + direction))
                    _hexNeighbors[coords].Add(coords + direction);
            }
            
            return _hexNeighbors[coords];
        }
    }
}
