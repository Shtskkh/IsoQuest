using Effects;
using Hexagons;
using UnityEngine;

namespace Player
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        private GameObject _targetHex;

        private void Awake()
        {
            if (!mainCamera)
                mainCamera = Camera.main;
        }

        public void OnClick(Vector2 inputPosition)
        {
            if (!FindTarget(inputPosition, out _targetHex)) return;
            if (!_targetHex.GetComponent<Hex>()) return;

            var glowHighlight = _targetHex.GetComponent<GlowHighlight>();
            if (!glowHighlight) return;
            glowHighlight.ToggleGlowing();
        }

        private bool FindTarget(Vector2 inputPosition, out GameObject result)
        {
            var ray = mainCamera.ScreenPointToRay(inputPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                result = hit.collider.gameObject;
                return true;
            }

            result = null;
            return false;
        }
    }
}