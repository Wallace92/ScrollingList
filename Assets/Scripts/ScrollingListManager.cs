using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollingListManager : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public GameObject ItemPrefab;

    private void Start()
    {
        var item = GameObject.Instantiate(ItemPrefab, ScrollRect.content.transform);
    }
}