using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Entity which shpuld be picked up in irder tobe able to open an exit door
/// </summary>
public class KeyObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer mesh;
    private float UseTime;
    public KeysData data { get; private set; }
    public void Init(KeysData _data)
    {
        data = _data;
        UseTime = data.UseTime;
        mesh.material = data.MainMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PlayerInventory>() != null) 
        {
            PlayerInventory.instance.Add(this);
            gameObject.SetActive(false);
        }
    }
}
