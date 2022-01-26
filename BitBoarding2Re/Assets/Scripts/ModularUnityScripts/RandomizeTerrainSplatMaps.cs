//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;


//public class RandomizeTerrainSplatMaps : EditorWindow
//{
//    [MenuItem("Window/Edit Mode Functions")]
//    public static void ShowWindow()
//    {
//        GetWindow<RandomizeTerrainSplatMaps>("Terrain Splat Map Generator");
//    }

//    public void OnGUI()
//    {
//        if(GUILayout.Button("Randomize Terrain Splats"))
//        {
//            RandomizeSplats();
//        }
//    }

//    private void RandomizeSplats()
//    {
//        TerrainData terrainData = Terrain.activeTerrain.terrainData;
//        float[,,] splatmapData = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];
//        for (int y = 0; y < terrainData.alphamapHeight; y++)
//        {
//            for (int x = 0; x < terrainData.alphamapWidth; x++)
//            {
//                // read the height at this location
//                float height = terrainData.GetHeight(x, y);
//                // determine the mix of textures 1, 2 & 3 to use 
//                // (using a vector3, since it can be lerped & normalized)
//                Vector3 splat = new Vector3(0, 1, 0);
//                if (height > 25) {
//                    splat = Vector3.Lerp(splat, new Vector3(0, 0, 1), (height - 25f) * .05f);
//                } else {
//                    splat = Vector3.Lerp(splat, new Vector3(1, 0, 0), height * .05f);
//                }
//                // now assign the values to the correct location in the array
//                splat.Normalize();
                
//                splatmapData[x, y, 0] = splat.x;
//                splatmapData[x, y, 1] = splat.y;
//                splatmapData[x, y, 2] = splat.z;
//            }
//        }
//        terrainData.SetAlphamaps(0, 0, splatmapData);

//        AddAlphaNoise(Terrain.activeTerrain, 100);
//        AddAlphaNoise(Terrain.activeTerrain, 100);
//        AddAlphaNoise(Terrain.activeTerrain, 100);
//    }

//    void AddAlphaNoise(Terrain t, float noiseScale)
//    {
//        float[,,] maps = t.terrainData.GetAlphamaps(0, 0, t.terrainData.alphamapWidth, t.terrainData.alphamapHeight);

//        for (int y = 0; y < t.terrainData.alphamapHeight; y++)
//        {
//            for (int x = 0; x < t.terrainData.alphamapWidth; x++)
//            {
//                float a0 = maps[x, y, 0];
//                float a1 = maps[x, y, 1];

//                a0 += Random.value * noiseScale;
//                a1 += Random.value * noiseScale;

//                float total = a0 + a1;

//                maps[x, y, 0] = a0 / total;
//                maps[x, y, 1] = a1 / total;
//            }
//        }

//        t.terrainData.SetAlphamaps(0, 0, maps);
//    }
//}
