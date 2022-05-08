﻿using UnityEngine;
using UnityEngine.UI;

public class MateriReader : MonoBehaviour
{
    [SerializeField] public Image m_Container;
    [SerializeField] public Sprite[] m_Sprites;
    public int index = 0;

    void Start()
    {
        m_Container.sprite = m_Sprites[index];
    }

    public void ChangeImage(bool right)
    {
        if (right && index != m_Sprites.Length - 1)
        {
            index++;
            m_Container.sprite = m_Sprites[index];
        }
        if (!right && index != 0)
        {
            index--;
            m_Container.sprite = m_Sprites[index];
        }
    }
}