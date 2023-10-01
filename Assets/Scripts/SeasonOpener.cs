using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasonOpener : MonoBehaviour
{
    public int needPoints=0;

    [SerializeField] private GameObject panelRaces;
    [SerializeField] private UISelectableButton nextPanelSelect;

    private SeasonOpener nextOpener;
    private UISelectableButton selectableButton;
    private UIRaceButton[] raceButtons;

    private int sumPoints;
    void Start()
    {
        selectableButton = GetComponent<UISelectableButton>();
        raceButtons= panelRaces.GetComponentsInChildren<UIRaceButton>(true);

        if(nextPanelSelect!=null)
        nextOpener = nextPanelSelect.GetComponent<SeasonOpener>();

        AwakeRaceButtonsCups();

        CalculateCupsOnCurrentSeason();

        TryCloseNextSelect();
    }
    private void AwakeRaceButtonsCups()
    {
        foreach (var race in raceButtons)
        {
            race.EarlyAwake();
        }
    }
   private void CalculateCupsOnCurrentSeason()
    {
        foreach (var race in raceButtons)
        {
            sumPoints += race.Cups;
        }
    }
    private void TryCloseNextSelect()
    {
        if (nextOpener != null)
        {
            if (sumPoints <= nextOpener.needPoints || raceButtons[raceButtons.Length - 1].Cups == 0 || selectableButton.Interactable==false) nextPanelSelect.Interactable = false;
        }
        
    }
}
