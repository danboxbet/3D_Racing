using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceButtonConteiner : MonoBehaviour
{
    [SerializeField] private UIRaceButton[] UI_RaceButtons;
    private List<RaceInfo> raceInfos = new List<RaceInfo>();
    private void Start()
    {
        for(int i=0;i<UI_RaceButtons.Length;i++)
        {
            
            if (UI_RaceButtons[i].Cups == 2 && i < UI_RaceButtons.Length - 1)
            {
               
                raceInfos.Add(UI_RaceButtons[i + 1].RaceInfo);
                UI_RaceButtons[i + 1].Interactable = true;
            }
            else if (i+1 < UI_RaceButtons.Length)
            {   
              UI_RaceButtons[i + 1].Interactable = false;

            }
                
            
        }
    }
}
