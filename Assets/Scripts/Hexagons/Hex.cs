using UnityEngine;
using Hexagons;
using UnityEditor.Localization.Plugins.XLIFF.V12;

namespace Hexagons
{
    public class Hex : MonoBehaviour
    {
        [SerializeField] private Vector3Int hexCoords;

        private float _hexSize;

        private void Awake()
        {
            _hexSize = GetComponent<MeshRenderer>().bounds.size.z * 0.5f;
            var coords = HexCoords.FromPosition(transform.position, _hexSize);
            hexCoords = new Vector3Int(coords.Q, coords.R, coords.S);
        }
    }
}