using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dependency : MonoBehaviour
{
    protected virtual void Bind(MonoBehaviour mono)
    {

    }
   protected void FindAllObjectToBind()
    {
        MonoBehaviour[] monoInScene = FindObjectsOfType<MonoBehaviour>();

        for (int i = 0; i < monoInScene.Length; i++)
        {
            Bind(monoInScene[i]);

        }
    }
    protected void Bind<T>(MonoBehaviour bindObject, MonoBehaviour targer) where T:class
    {
        if (targer is IDependency<T>) (targer as IDependency<T>).Construct(bindObject as T);
    }
}
