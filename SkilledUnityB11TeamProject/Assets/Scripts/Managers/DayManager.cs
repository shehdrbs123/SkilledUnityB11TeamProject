using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DayManager : MonoBehaviour
{
    [Header("Date")]
    public int day = 0;
    [Range(0.0f, 1.0f)] public float time;
    public bool isNight = false;

    [Header("Setting")]
    public float fullDayLength;
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

    [Header("UI")]
    public TextMeshProUGUI textDay;

    private float dayStart = 0.3f;
    private float nightStart = 0.7f;

    private void Start()
    {
        timeRate = 1.0f / fullDayLength;
        time = dayStart;
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        if (isNight && !(time <= dayStart || nightStart <= time))
        {
            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);
            isNight = false;
            day += 1;
            textDay.text = day.ToString();
        }
        else if (!isNight && (time <= dayStart || nightStart <= time))
        {
            sun.gameObject.SetActive(false);
            moon.gameObject.SetActive(true);
            isNight = true;
        }

        RenderSettings.skybox = isNight ? moonSkybox : sunSkybox;

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
    }
}
