using System;
using UnityEngine;

namespace QbGameLib.Render.Component
{
    [RequireComponent(typeof(Renderer))]
    public class MaterialPropertyBlockComponent : MonoBehaviour
    {
        private MaterialPropertyBlock _materialPropertyBlock;
        private Renderer _renderer;

        public MaterialPropertyBlock MaterialPropertyBlock => _materialPropertyBlock;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
            _materialPropertyBlock = new MaterialPropertyBlock();
            _renderer.SetPropertyBlock(_materialPropertyBlock);
        }

        public void UpdatePropertyBlock(int materialIndex = 0)
        {
            _renderer.SetPropertyBlock(_materialPropertyBlock,materialIndex);
        }
        
        public void SetPropertyBlock(MaterialPropertyBlock block, int materialIndex = 0)
        {
            _renderer.SetPropertyBlock(block,materialIndex);
        }
        
    }
}