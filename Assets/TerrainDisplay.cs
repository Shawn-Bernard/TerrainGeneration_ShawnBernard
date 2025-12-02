using UnityEngine;
using UnityEngine.UIElements;

public class TerrainDisplay : MonoBehaviour
{
    public Renderer textureRender;
    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    /// <summary>
    /// Takes in texture and applies to material and re-scales to fit 
    /// </summary>
    /// <param name="texture"></param>
    public void DrawTexture(Texture2D texture)
    {

        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData meshData,Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
