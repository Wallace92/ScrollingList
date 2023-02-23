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
    public Task<List<Sprite>> Load()
    {
        var tcs = new TaskCompletionSource<List<Sprite>>();
        
        var m_sprites = new List<Sprite>();
        DateTime today = DateTime.Now;

        var info = new DirectoryInfo(spritesPath);
        
        var fileInfo = info.GetFiles().
            Where(file => file.Extension != ".meta");
        
        foreach (var file in fileInfo)
        {
            var difference = (today - file.CreationTime).ToString("hh\\:mm");;
            Debug.Log(file.Name + " " + difference);
            m_sprites.Add(LoadSprite(spritesPath + file.Name));
        }
        
        tcs.SetResult(m_sprites);

        return tcs.Task;
    }
    
    
    public Sprite LoadSprite(string file)
    {
        AssetDatabase.ImportAsset(file);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(file);

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}