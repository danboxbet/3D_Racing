using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInteractiveNextLevel : MonoBehaviour
{
    [SerializeField] private UIRaceButton nextButton;
    [SerializeField] private UIRaceButton lastButton;
    [SerializeField] private int needCup;
    public int NeedCup => needCup;
    private UIRaceButton raceButton;
    private UIInteractiveNextLevel interactiveNextLevel;

    private void Start()
    {
        
        raceButton = GetComponent<UIRaceButton>();
        interactiveNextLevel = raceButton.GetComponent<UIInteractiveNextLevel>();
        if (nextButton != null) CheckCup();
    }

    private void CheckCup()
    {
        
        if (raceButton.Cups >= interactiveNextLevel.NeedCup)
        {
           nextButton.Interactable = true;    
        }
        else nextButton.Interactable = false;

        if(!raceButton.Interactable)
        {
            nextButton.Interactable = false;
        }
    }
}
