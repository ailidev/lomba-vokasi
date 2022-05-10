using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupObjectManager : MonoBehaviour
{
    public static PickupObjectManager Instance { get; set; }
    public PickupObjectData m_PickUpObjectData;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    public void StartSearching(bool value)
    {
        m_PickUpObjectData.m_IsSearchingObject = value;
    }

}
