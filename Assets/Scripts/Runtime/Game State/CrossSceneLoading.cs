using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CrossSceneLoading : Singleton<CrossSceneLoading>
{
    public HatData CurrentHat = null;
    public UnityEvent<HatData> OnHatSet =  new UnityEvent<HatData>();

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);

    }

    public void SetHat(Transform hatTransform)
    {
        CurrentHat = new HatData()
        {
            Mesh = hatTransform.GetComponent<MeshFilter>().mesh,
            Materials = hatTransform.GetComponent<MeshRenderer>().materials,
            Rotation = hatTransform.eulerAngles,
            Scale = hatTransform.localScale,
        };
        OnHatSet?.Invoke(CurrentHat);
    }

    public void RemoveHat()
    {
        CurrentHat = null;
        OnHatSet?.Invoke(CurrentHat);
    }
}

public class HatData
{
    public Mesh Mesh; 
    public Material[] Materials;
    public Vector3 Rotation;
    public Vector3 Scale;
}
