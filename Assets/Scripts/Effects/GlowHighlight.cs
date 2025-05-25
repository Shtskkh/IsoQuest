using UnityEngine;

namespace Effects
{
    public class GlowHighlight : MonoBehaviour
    {
        private static readonly int Texture1 = Shader.PropertyToID("_Texture");
        [SerializeField] private Material glowMaterial;
        [SerializeField] private bool isGlowing;

        private MeshRenderer _renderer;
        private Material _originalMaterial;

        private void Awake()
        {
            _renderer = GetComponent<MeshRenderer>();
            _originalMaterial = _renderer.material;
            glowMaterial.SetTexture(Texture1, _originalMaterial.mainTexture);
        }

        private void Update()
        {
            _renderer.material = isGlowing ? glowMaterial : _originalMaterial;
        }

        public void ToggleGlowing()
        {
            isGlowing = !isGlowing;
        }
    }
}