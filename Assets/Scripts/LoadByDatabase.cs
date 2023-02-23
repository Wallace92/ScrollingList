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
    
    public async Task<List<Sprite>> Load()
    {
        var sprites = new List<Sprite>();
        
        DateTime today = DateTime.Now;

        var info = new DirectoryInfo(spritesPath);
        
        var fileInfo = info.GetFiles().
            Where(file => file.Extension != ".meta");
        
        foreach (var file in fileInfo)
        {
            var difference = (today - file.CreationTime).ToString("dd\\:hh\\:mm");;
            Debug.Log(file.Name + " " + difference);
            sprites.Add(await LoadSprite(spritesPath + file.Name));
        }
        
        return sprites;
    }
    
    
    private async Task<Sprite> LoadSprite(string file)
    {
        AssetDatabase.ImportAsset(file);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(file);

        return await LoaderHelper.CreateSprite(texture);
    }
}