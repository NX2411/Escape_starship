using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class pixelRenderer : MonoBehaviour
{
    [Range(1, 100)]
    public int pixelate;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        RenderTexture renderTexture = RenderTexture.GetTemporary(source.width / pixelate, source.height / pixelate, 0, source.format);
        renderTexture.filterMode = FilterMode.Point;
        Graphics.Blit(source, renderTexture);
        Graphics.Blit(renderTexture, destination);
        RenderTexture.ReleaseTemporary(renderTexture);
    }
}
