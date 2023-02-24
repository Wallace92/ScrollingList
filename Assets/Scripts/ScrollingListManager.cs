using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ScrollingListManager : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public List<ItemData> ItemsData = new List<ItemData>();
    public Image Image;
    public ScrollRect ScrollRect;
    public GameObject ItemPrefab;
    public string spritesPath;
    public bool LoadUsingAddressables;

    private ILoad Loader => LoadUsingAddressables ? new LoadByAddressable() : new LoadByDatabase();
    private IRefresh Refresher => LoadUsingAddressables ? new RefreshByAddressable() : new RefreshByDatabase();

    private Item m_item;
    private async void Start()
    {

        ItemsData = await Loader.LoadItemsData();
        
        var itemGameObject = Instantiate(ItemPrefab, ScrollRect.content.transform);
        m_item = itemGameObject.GetComponent<Item>();
      
        m_item.AssignData(ItemsData.Last());
    }

    public void Refresh() => RefreshAsync();

    public async void RefreshAsync()
    {
        ItemsData = await Refresher.Refresh(Loader);
        m_item.AssignData(ItemsData.Last());
    }
}