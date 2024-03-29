﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
    
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialSize;

    public readonly Stack<GameObject> Instances = new Stack<GameObject>();
    
    private void Awake()
    {
        Assert.IsNotNull(prefab);
    }
    
    public void Initialize()
    {
        for (var i = 0; i < initialSize; i++)
        {
            var obj = CreateInstance();
            obj.SetActive(false);
            Instances.Push(obj);
        }
    }
    
    public GameObject GetObject()
    {
        var obj = Instances.Count > 0 ? Instances.Pop() : CreateInstance();
        obj.SetActive(true);
        return obj;
    }
    
    public void ReturnObject(GameObject obj)
    {
        var pooledObject = obj.GetComponent<PooledObject>();
        Assert.IsNotNull(pooledObject); 
        Assert.IsTrue(pooledObject.pool == this);

        obj.transform.position= new Vector3(0f,0f,0f);
        obj.SetActive(false);
        if (!Instances.Contains(obj))
        {
            Instances.Push(obj);
        }
    }

    public void Reset()
    {
        var objectsToReturn = new List<GameObject>();
        foreach (var instance in transform.GetComponentsInChildren<PooledObject>())
        {
            if (instance.gameObject.activeSelf)
            {
                objectsToReturn.Add(instance.gameObject);
            }
        }
        foreach (var instance in objectsToReturn)
        {
            ReturnObject(instance);
        }
    }
    
    private GameObject CreateInstance()
    {   
        var obj = Instantiate(prefab);
        var pooledObject = obj.AddComponent<PooledObject>();
        pooledObject.pool = this;
        obj.transform.SetParent(transform);
        return obj;
    }
}

public class PooledObject : MonoBehaviour
{
    public ObjectPool pool;
}

