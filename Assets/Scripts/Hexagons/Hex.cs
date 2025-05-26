using Effects;
using UnityEngine;

namespace Hexagons
{
    public class Hex : MonoBehaviour
    {
        [SerializeField] public Vector3Int hexCoords;
        [SerializeField] public GlowHighlight highlight;

        private void Awake()
        {
            var coords = HexCoords.FromPosition(transform.position, HexMetrics.HexSize);
            hexCoords = new Vector3Int(coords.Q, coords.R, coords.S);
        }

        public void ToggleHighlight()
        {
            highlight.ToggleGlowing();
        }

        public void EnableHighlight()
        {
            highlight.EnableGlowing();
        }

        public void DisableHighlight()
        {
            highlight.DisableGlowing();
        }
    }
}