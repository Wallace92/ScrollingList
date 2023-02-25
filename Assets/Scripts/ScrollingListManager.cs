using System.Collections.Generic;
using UnityEngine;

public class ScrollingListManager : MonoBehaviour
{
    [SerializeField]
    private bool m_loadUsingAddressables;
    
    private List<Item> m_scrollList = new List<Item>();

    private ILoad Loader => m_loadUsingAddressables
        ? new LoadByAddressable(m_contentData.Label)
        : new LoadByDatabase(m_contentData.Path);

    private IRefresh Refresher => m_loadUsingAddressables
        ? new RefreshByAddressable(m_contentData)
        : new RefreshByDatabase();

    private ItemPool m_itemPool;
    
    private readonly ContentData m_contentData = new ContentData("Assets/Content/", "Content", "item");
    
    public void Refresh() => RefreshAsync();

    private void Awake() => m_itemPool = gameObject.GetComponent<ItemPool>();

    private async void Start()
    {
        LoadingProgress.ProgressBarVisibility.Invoke(true);
        var itemsData = await Refresher.Refresh(Loader);
        m_scrollList = InstantiateScrollList(itemsData);
    }

    private List<Item> InstantiateScrollList(List<ItemData> itemsData)
    {
        var scrollList = new List<Item>();

        foreach (var itemData in itemsData)
        {
            var item = m_itemPool.ItemsPool.Get();
            item.AssignData(itemData);
            scrollList.Add(item);
        }
        
        return scrollList;
    }

    private async void RefreshAsync()
    {
         LoadingProgress.ProgressBarVisibility.Invoke(true);
         var itemsData = await Refresher.Refresh(Loader);
         
         foreach (var item in m_scrollList)
             Destroy(item.gameObject);
         
         m_scrollList.Clear();
         
         m_scrollList = InstantiateScrollList(itemsData);
    }
}