using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LoadByAddressable : ILoad
{
    private readonly string m_label;
    
    public LoadByAddressable(string label) => m_label = label;

    public async Task<List<ItemData>> LoadItemsData()
    {
        var locations = await Addressables.LoadResourceLocationsAsync(m_label).Task;
        
        var itemsData = new List<ItemData>();

        DateTime today = DateTime.Now;

        foreach (var location in locations)
        {
            var path = location.InternalId;
            var filePath = Application.dataPath + "/" + path.Replace("Assets", "");
            var fileInfo = new FileInfo(filePath);
           
            var textureHandle  = Addressables.LoadAssetAsync<Texture2D>(location);
            var texture = await textureHandle.Task;
            
            var difference = (today - fileInfo.CreationTime).ToString("dd\\:hh\\:mm");
            var sprite = await LoaderHelper.CreateSprite(texture);
            
            var itemData = new ItemData(fileInfo.Name, difference, sprite);
            
            itemsData.Add(itemData);
        }

        return itemsData;
    }
}