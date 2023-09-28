using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    [Header("Date")]
    public int day = 0;
    [Range(0.0f, 1.0f)] public float time;
    public bool isNight = false;

    [Header("Setting")]
    public float fullDayLength;
    public float startTime;
    private float timeRate;
    public Vector3 noon;

    [Header("Sun")]
    public Light sun;
    public Gradient sunColor;
    public AnimationCurve sunIntensity;
    public Material sunSkybox;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    public Material moonSkybox;

    [Header("Other Lighting")]
    public AnimationCurve lightingIntensityMultiplier;
    public AnimationCurve reflectionIntensityMultiplier;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = startTime;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        if (isNight && !(time <= 0.25f || 0.75f <= time))
        {
            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);
            isNight = false;
            day += 1;
        }
        else if (!isNight && (time <= 0.25f || 0.75f <= time))
        {
            sun.gameObject.SetActive(false);
            moon.gameObject.SetActive(true);
            isNight = true;
        }

        RenderSettings.skybox = isNight ? moonSkybox : sunSkybox;

        //UpdateLighting(sun, sunColor, sunIntensity);
        //UpdateLighting(moon, moonColor, moonIntensity);

        if (isNight) UpdateLighting(moon, moonColor, moonIntensity);
        else UpdateLighting(sun, sunColor, sunIntensity);

        RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        RenderSettings.reflectionIntensity = reflectionIntensityMultiplier.Evaluate(time);
    }

    private void UpdateLighting(Light lightSource, Gradient colorGradient, AnimationCurve intensityCurve)
    {
        float intensity = intensityCurve.Evaluate(time);

        lightSource.transform.eulerAngles = (time - (lightSource == sun ? 0.25f : 0.75f)) * 4.0f * noon;
        lightSource.color = colorGradient.Evaluate(time);
        lightSource.intensity = intensity;

        //GameObject go = lightSource.gameObject;
        //if (lightSource.intensity == 0 && go.activeInHierarchy)
        //{
        //    go.SetActive(false);
        //}
        //else if (lightSource.intensity > 0 && !go.activeInHierarchy)
        //{
        //    go.SetActive(true);

        //    if (lightSource == sun)
        //        day += 1;
        //}
    }
}
