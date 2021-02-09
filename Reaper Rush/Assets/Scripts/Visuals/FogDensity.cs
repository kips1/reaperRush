using UnityEngine;
using System.Collections;

/*
 * Author: Kips
 * 
 * Controls the density of the fog
 * 
 * Version:
 * 
 */

public class FogDensity : MonoBehaviour
{
    public Color fogColor;
    public Color ambientLight;
    Color previousFogColor;
    Color previousAmbientLight;
    public float fogDensity;
    public float haloStrength;
    public float flareStrength;
    float previousFogDensity;
    float previousHaloStrength;
    float previousFlareStrength;

    public bool fog;
    bool previousFog;

    void OnPreRender()
    {
        previousFog = RenderSettings.fog;
        previousFogColor = RenderSettings.fogColor;
        previousFogDensity = RenderSettings.fogDensity;
        previousAmbientLight = RenderSettings.ambientLight;
        previousHaloStrength = RenderSettings.haloStrength;
        previousFlareStrength = RenderSettings.flareStrength;
        if (fog)
        {
            RenderSettings.fog = fog;
            RenderSettings.fogColor = fogColor;
            RenderSettings.fogDensity = fogDensity;
            RenderSettings.ambientLight = ambientLight;
            RenderSettings.haloStrength = haloStrength;
            RenderSettings.flareStrength = flareStrength;
        }
    }

    void OnPostRender()
    {
        RenderSettings.fog = previousFog;
        RenderSettings.fogColor = previousFogColor;
        RenderSettings.fogDensity = previousFogDensity;
        RenderSettings.ambientLight = previousAmbientLight;
        RenderSettings.haloStrength = previousHaloStrength;
        RenderSettings.flareStrength = previousFlareStrength;
    }
}