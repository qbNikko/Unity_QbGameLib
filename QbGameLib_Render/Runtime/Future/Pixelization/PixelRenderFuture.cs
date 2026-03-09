using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace QbGameLib.Render.Future.Pixelization
{
    [Serializable]
    public class PixelRenderSettings
    {
        [Header("Render Settings")]
        public RenderPassEvent renderPass = RenderPassEvent.BeforeRenderingPostProcessing;
        
        [Header("Pixelization Settings")]
        public int height = 240;
        public Material material;
        public int shaderPassIndex = 0;
        
        public string passName = "Pixelization";

    }
    public class PixelRenderFuture : ScriptableRendererFeature
    {
        [SerializeField] private PixelRenderSettings settings;
        private PixelRenderPass _pass;
        
        public override void Create()
        {
            _pass = new PixelRenderPass(settings);
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
#if UNITY_EDITOR
            if(renderingData.cameraData.isSceneViewCamera) return;
#endif
            renderer.EnqueuePass(_pass);
        }
    }
}