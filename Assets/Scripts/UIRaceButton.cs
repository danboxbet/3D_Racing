using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class UIRaceButton : UISelectableButton
{
    [SerializeField] private RaceInfo raceInfo;

    [SerializeField] private Image icon;
    [SerializeField] private Text title;

    [SerializeField] private List<Image> imagesCups;

    public RaceInfo RaceInfo => raceInfo;

    private int cups;
    public int Cups => cups;
    public void EarlyAwake()
    {
        cups = PlayerPrefs.GetInt(raceInfo.SceneName + "result");
    }
    private void Awake()
    {
        ApplyProperty(raceInfo);
        cups = PlayerPrefs.GetInt(raceInfo.SceneName + "result");
       
        PaintCups();
        
    }
    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);

        if (raceInfo == null) return;
        if (!Interactable) return;
        SceneManager.LoadScene(raceInfo.SceneName);
    }
    public void ApplyProperty(RaceInfo property)
    {
        if (property == null) return;

        raceInfo = property;

        icon.sprite = raceInfo.Sprite;
        title.text = raceInfo.Title;

    }
    [ContextMenu("DeleteResult")]
    public void DeleteResult()
    {
        PlayerPrefs.DeleteKey(raceInfo.SceneName + "result");
        PlayerPrefs.DeleteKey(raceInfo.SceneName+"_player_best_time");
    }
    private void PaintCups()
    {
        for(int i=0; i<cups;i++)
        {
            imagesCups[i].color = Color.white;
        }
    }
}
