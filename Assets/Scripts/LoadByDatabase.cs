using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class LoadByDatabase : ILoad
{
    private string spritesPath = "Assets/Sprites/";

    public async Task<List<ItemData>> LoadItemsData()
    {
        var itemsData = new List<ItemData>();
        var fileInfo = LoaderHelper.GetFileInfo(spritesPath);
        
        DateTime today = DateTime.Now;
        
        foreach (var file in fileInfo)
        {
            var difference = (today - file.CreationTime).ToString("dd\\:hh\\:mm");
            var sprite = await LoadSprite(spritesPath + file.Name);
            var itemData = new ItemData(file.Name, difference, sprite);
            
            itemsData.Add(itemData);
        }

        return itemsData;
    }

    
    
    private async Task<Sprite> LoadSprite(string file)
    {
        AssetDatabase.ImportAsset(file);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(file);

        return await LoaderHelper.CreateSprite(texture);
    }
}