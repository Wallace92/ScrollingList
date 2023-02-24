using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingListManager : MonoBehaviour
{
    public ScrollRect ScrollRect;
    public GameObject ItemPrefab;

    [SerializeField]
    private bool m_loadUsingAddressables;
    
    private List<Item> scrollList = new List<Item>();

    private ILoad Loader => m_loadUsingAddressables
        ? new LoadByAddressable(m_contentData.Label)
        : new LoadByDatabase(m_contentData.Path);

    private IRefresh Refresher => m_loadUsingAddressables
        ? new RefreshByAddressable(m_contentData)
        : new RefreshByDatabase();

    private readonly ContentData m_contentData = new ContentData("Assets/Content/", "Content", "item");

    private async void Start()
    {
        var itemsData = await Refresher.Refresh(Loader);

        foreach (var itemData in itemsData)
        {
            var itemGameObject = Instantiate(ItemPrefab, ScrollRect.content.transform);
            var item = itemGameObject.GetComponent<Item>();
            item.AssignData(itemData);
            
            scrollList.Add(item);
        }
    }

    public void Refresh() => RefreshAsync();

    private async void RefreshAsync()
    {
         var itemsData = await Refresher.Refresh(Loader);

         foreach (var item in scrollList)
         {
             Destroy(item.gameObject);
         }
         scrollList.Clear();
         
         
         foreach (var itemData in itemsData)
         {
             var itemGameObject = Instantiate(ItemPrefab, ScrollRect.content.transform); 
             var item = itemGameObject.GetComponent<Item>(); 
             item.AssignData(itemData);
             
             scrollList.Add(item);
         }
    }
}