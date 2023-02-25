﻿using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private IObjectPool<Item> objectPool;
    public IObjectPool<Item> ObjectPool
    {
        set => objectPool = value;
    }

    public TextMeshProUGUI NameTMP;
    public TextMeshProUGUI TimeTMP;
    public Image Image;

    public void AssignData(ItemData itemData)
    {
        NameTMP.text = itemData.Name;
        TimeTMP.text = itemData.Time;
        Image.sprite = itemData.Image;
    }
}