using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerHat : MonoBehaviour
{
    [SerializeField] private MeshRenderer _hat;

    private void Start()
    {
        CrossSceneLoading.Instance.OnHatSet.AddListener(UpdateHat);
        UpdateHat(CrossSceneLoading.Instance.CurrentHat);
    }

    private void UpdateHat(HatData data)
    {
        if (data == null)
        {
            print("null HatData");
            _hat.GetComponent<MeshFilter>().mesh = null;
            return;
        }
        _hat.materials = data.Materials;
        _hat.GetComponent<MeshFilter>().mesh = data.Mesh;
        _hat.transform.eulerAngles = data.Rotation;
        _hat.transform.localScale = data.Scale;
    }
}
