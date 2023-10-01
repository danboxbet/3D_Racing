using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu]
public class AudioMixerFloatSettings : Setting
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private string nameSetting;

    [SerializeField] private float step;
    [SerializeField] private float minRealValue;
    [SerializeField] private float maxRealValue;

    [SerializeField] private float minVirtualValue;
    [SerializeField] private float maxVirtualValue;

    private float currentValue = 0;

    public override bool isMinValue { get => currentValue == minRealValue; }
    public override bool isMaxValue { get => currentValue == maxRealValue; }


    public override void SetNextValue()
    {
        AddValue(Mathf.Abs(maxRealValue-minRealValue)/step);
    }
    public override void SetPreviousValue()
    {
        AddValue(-Mathf.Abs(maxRealValue - minRealValue) / step);
    }
    public override string GetStringValue()
    {
        return Mathf.Lerp(minVirtualValue, maxVirtualValue, (currentValue - minRealValue) / (maxRealValue - minRealValue)).ToString();
    }
    private void AddValue(float value)
    {
        currentValue += value;
        currentValue = Mathf.Clamp(currentValue, minRealValue, maxRealValue);
    }
    public override object GetValue()
    {
        return currentValue;
    }
    public override void Apply()
    {
       
        audioMixer.SetFloat(nameSetting, currentValue);

        Save();
    }
    public override void Load()
    {
       
       currentValue= PlayerPrefs.GetFloat(title, 0);
    }
    private void Save()
    {
        
        PlayerPrefs.SetFloat(title, currentValue);
    }
}
