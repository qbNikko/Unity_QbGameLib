using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RenderGraphModule;
using UnityEngine.Rendering.RenderGraphModule.Util;
using UnityEngine.Rendering.Universal;

namespace QbGameLib.Render.Future.Pixelization
{
    public class PixelRenderPass : ScriptableRenderPass
    {
        private PixelRenderSettings _settings;
        private Material _material;
        private int screenHeight, screenWidth;
        
        private string textureName;

        public PixelRenderPass(PixelRenderSettings settings)
        {
            _settings = settings;
            renderPassEvent = settings.renderPass;
            if(_material==null) _material = _settings.material;
            screenHeight = _settings.height;
            requiresIntermediateTexture = true;
            textureName = "Texture-" + _settings.passName;
        }

        public override void RecordRenderGraph(RenderGraph renderGraph, ContextContainer frameData)
        {
            if (_material == null) return;
            var cameraData = frameData.Get<UniversalCameraData>();
            var resourceData = frameData.Get<UniversalResourceData>();
            TextureHandle srcColor = resourceData.activeColorTexture; 
            screenWidth = Mathf.Max(1, (int)(screenHeight*cameraData.camera.aspect+0.5f));
            
            var destinationDesc = renderGraph.GetTextureDesc(resourceData.activeColorTexture);
            destinationDesc.name = textureName;
            destinationDesc.width = screenWidth;
            destinationDesc.height = _settings.height;
            destinationDesc.depthBufferBits = 0;
            destinationDesc.filterMode = FilterMode.Point;
            destinationDesc.wrapMode = TextureWrapMode.Clamp;
            destinationDesc.clearBuffer = false;

            _material.SetVector("_BlockCount", new Vector2(screenWidth,screenHeight));
            _material.SetVector("_BlockSize", new Vector2(1f/screenWidth,1f/screenHeight));
            _material.SetVector("_HalfBlockSize", new Vector2(0.5f/screenWidth, 0.5f/screenHeight));

            TextureHandle destination = renderGraph.CreateTexture(destinationDesc);
            renderGraph.AddBlitPass(
                new RenderGraphUtils.BlitMaterialParameters(srcColor, destination, _material, _settings.shaderPassIndex), 
                _settings.passName
            );
            resourceData.cameraColor = destination;
        }
    }
}