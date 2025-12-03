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
    /// <summary>
    /// Uses the mesh data to return a mesh and applies the texture
    /// </summary>
    /// <param name="meshData"></param>
    /// <param name="texture"></param>
    public void DrawMesh(MeshData meshData,Texture2D texture)
    {
        meshFilter.sharedMesh = meshData.CreateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
