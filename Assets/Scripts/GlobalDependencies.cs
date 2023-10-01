using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GlobalDependencies : Dependency
{
    public bool IsDontDestroy;
    [SerializeField] private Pauser pauser;
    private static GlobalDependencies instance;

    private void Awake()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        if(IsDontDestroy)
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    protected override void Bind(MonoBehaviour mono)
    {
        base.Bind(mono);
        pauser = FindObjectOfType<Pauser>();
        Bind<Pauser>(pauser, mono);
    }
    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        FindAllObjectToBind();
    }
}
