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
    public AudioClip dayBGM;

    [Header("Moon")]
    public Light moon;
    public Gradient moonColor;
    public AnimationCurve moonIntensity;
    public Material moonSkybox;
    public AudioClip nightBGM;

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
        TalkingMyself(day);
    }

    private void Update()
    {
        time = (time + timeRate * Time.deltaTime) % 1.0f;

        if (isNight && !(time <= dayStart || nightStart <= time))
        {
            day += 1;

            sun.gameObject.SetActive(true);
            moon.gameObject.SetActive(false);
            isNight = false;
            SoundManager.ChangeBackGroundMusic(dayBGM);
            TalkingMyself(day);
            
            if (day <= 5) textDay.text = day.ToString();
            else textDay.text = "????";
        }
        else if (!isNight && (time <= dayStart || nightStart <= time))
        {
            sun.gameObject.SetActive(false);
            moon.gameObject.SetActive(true);
            isNight = true;
            SoundManager.ChangeBackGroundMusic(nightBGM);
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

    private void TalkingMyself(int _day)
    {
        string conv = _day switch
        {
            1 => "이 곳도 이제 지긋지긋하다...\n지킬건 이 집뿐이다.",
            2 => "5일에 가족들이 오기로 했다는 소식이 왔다.\n집을 더 안전하게 해야해...",
            3 => "가족을 위해서 집을 지켜야 하는데.\n정신을 차릴수가 없다..",
            4 => "내일이면 가족들을 볼 수 있다.\n힘내보자...",
            5 => "오늘이 4일 아니 5일이다..\n오늘이 왔다...",
            6 => "왜 가족들은 아무 연락이 없는거지...\n무슨 일이 일어난 걸까?",
            _ => "",
        };

        UIManager.PopupText(conv);
    }
}
