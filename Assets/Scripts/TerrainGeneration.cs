using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap,ColorMap, Mesh};
    public DrawMode drawMode;

    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelOfDetail;// Skips for loops intersection when doing mesh
    public float noiseScale;

    [Range(1,29)]
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    public bool autoUpdate;
    public bool randomSeed;

    public TerrainType[] regions;

    private void Start()
    {
        GenerateTerrain();
    }

    public void GenerateTerrain()
    {
        if (randomSeed)
        {
            seed = Random.Range(0, 1000);
        }
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, mapChunkSize,seed,noiseScale,octaves,persistance,lacunarity,offset);
        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        //Loops through the map and checks height and applies the region based of the height
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x,y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapChunkSize + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        TerrainDisplay terrainDisplay = FindAnyObjectByType<TerrainDisplay> ();
        if (drawMode == DrawMode.NoiseMap)
        {
            terrainDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            terrainDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap,mapChunkSize,mapChunkSize));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            terrainDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHeightMultiplier, meshHeightCurve,levelOfDetail), TextureGenerator.TextureFromColorMap(colorMap, mapChunkSize, mapChunkSize));
        }
    }

    private void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 1)
        {
            octaves = 1;
        }
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}