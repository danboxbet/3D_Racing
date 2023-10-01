using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ResoulutionSetting : Setting
{
    [SerializeField]
    private Vector2Int[] avalibaleResolutions = new Vector2Int[]
    {
       new Vector2Int(800,900),
       new Vector2Int(1280,720),
       new Vector2Int(1600,900),
       new Vector2Int(1920,1080),
   };
    private int currentResolutionIndex=0;

    public override bool isMinValue { get => currentResolutionIndex == 0; }
    public override bool isMaxValue { get => currentResolutionIndex == avalibaleResolutions.Length-1; }

    public override void SetNextValue()
    {
        if(currentResolutionIndex<avalibaleResolutions.Length-1)
        {
            currentResolutionIndex++;
        }
    }
    public override void SetPreviousValue()
    {
        if (currentResolutionIndex >0)
        {
            currentResolutionIndex--;
        }
    }
    public override object GetValue()
    {
        return avalibaleResolutions[currentResolutionIndex];
    }
    public override string GetStringValue()
    {
        return avalibaleResolutions[currentResolutionIndex].x + "x" + avalibaleResolutions[currentResolutionIndex].y;
    }
    public override void Apply()
    {
        Screen.SetResolution(avalibaleResolutions[currentResolutionIndex].x, avalibaleResolutions[currentResolutionIndex].y, true);

        Save();
    }
    public override void Load()
    {
        currentResolutionIndex = PlayerPrefs.GetInt(title, avalibaleResolutions.Length - 1);
    }
    private void Save()
    {
        PlayerPrefs.SetInt(title, currentResolutionIndex);
    }
}
