using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class EnigneIndicatorColor
{
    public float MaxRpm;
    public Color color;
}
public class CarEngineIndicator : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private Image image;
    [SerializeField] private EnigneIndicatorColor[] colors;

    private void Update()
    {
        image.fillAmount = car.EngineRpm / car.EngineMaxRpm *2;
        for(int i=0;i<colors.Length;i++)
        {
            if(car.EngineRpm<=colors[i].MaxRpm)
            {
                image.color = colors[i].color;
                break;
            }
        }
    }
}
