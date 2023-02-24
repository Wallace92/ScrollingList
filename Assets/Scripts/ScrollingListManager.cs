using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingListManager : MonoBehaviour
{
    public List<ItemData> ItemsData = new List<ItemData>();
    public ScrollRect ScrollRect;
    public GameObject ItemPrefab;
    
    [SerializeField]
    private bool m_loadUsingAddressables;

    private ILoad Loader => m_loadUsingAddressables ? new LoadByAddressable() : new LoadByDatabase();
    private IRefresh Refresher => m_loadUsingAddressables
        ? new RefreshByAddressable("Assets/Content/", "Content", "item")
        : new RefreshByDatabase();

    private Item m_item;
    private async void Start()
    {

        ItemsData = await Refresher.Refresh(Loader);
        
        var itemGameObject = Instantiate(ItemPrefab, ScrollRect.content.transform);
        m_item = itemGameObject.GetComponent<Item>();
      
        m_item.AssignData(ItemsData.Last());
    }

    public void Refresh() => RefreshAsync();

    private async void RefreshAsync()
    {
        ItemsData = await Refresher.Refresh(Loader);
        m_item.AssignData(ItemsData.Last());
    }
}