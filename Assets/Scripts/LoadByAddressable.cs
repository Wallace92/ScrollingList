using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public class LoadByAddressable : ILoad
{
    private readonly string m_label;
    
    public LoadByAddressable(string label) => m_label = label;

    public async Task<List<ItemData>> LoadItemsData()
    {
        var locations = await Addressables
            .LoadResourceLocationsAsync(m_label)
            .Task;
        
        var itemsData = new List<ItemData>();

        DateTime today = DateTime.Now;

        foreach (var location in locations)
        {
            var fileInfo = GetFileInfo(location);

            var sprite = await LoaderHelper.LoadSprite(location);

            itemsData.Add(GetItemData(today, fileInfo, sprite));
            
            LoadingProgress.SetProgressBarValues(locations.Count, locations.IndexOf(location) + 1);
        }

        return itemsData;
    }

    private ItemData GetItemData(DateTime today, FileInfo fileInfo, Sprite sprite)
    {
        var difference = (today - fileInfo.CreationTime).ToString("dd\\:hh\\:mm");
        return new ItemData(fileInfo.Name, difference, sprite);
    }

    private FileInfo GetFileInfo(IResourceLocation location)
    {
        var path = location.InternalId;
        var filePath = Application.dataPath + "/" + path.Replace("Assets", "");
        return new FileInfo(filePath);
    }
}