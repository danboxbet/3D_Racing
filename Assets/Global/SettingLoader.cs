using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    [SerializeField] private Setting[] settings;

    private void Start()
    {
       
        for(int i=0;i<settings.Length;i++)
        {
            settings[i].Load();
            settings[i].Apply();
        }
    }
}
