using System;
using System.Collections;
using UnityEngine;

public static class Noise {
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight,float scale, int octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];
        //The floor for scale is being set to .0001
        if (scale <= 0) {
            scale = 0.0001f;
        }
        //Creating the variables to set the minimuum and max value of the noise map
        //Which we use to scale the noise map between 1-0 at the end.
        float maxNoiseH = float.MinValue;
        float minNoiseH = float.MaxValue;
        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;
                
                for(int i =0; i<octaves;i++){
                    //getting the x and y cordinates based on the scale
                    //and the frequency the cordinates need to occur
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;
                    //This is taking the X and Y position of the map
                    //Assigning the perlin noise values to it
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY)*2-1;
                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > maxNoiseH) {
                    maxNoiseH = noiseHeight;
                }else if (noiseHeight < minNoiseH) {
                    minNoiseH = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }

        for (int y = 0; y < mapHeight; y++) {
            for (int x = 0; x < mapWidth; x++) {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseH, maxNoiseH, noiseMap[x, y]);
            }
        }

        return noiseMap;
    }
}
