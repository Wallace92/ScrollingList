using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string pngFilePath;
    public string spritesPath;
    public Image Image;

    public List<Sprite> Sprites = new List<Sprite>();
    private void Start()
    {
        AssignSprites();
    }

    public void AssignSprites()
    {
        DateTime today = DateTime.Now;

        var info = new DirectoryInfo(spritesPath);
        
        var fileInfo = info.GetFiles().
            Where(file => file.Extension != ".meta");
        foreach (var file in fileInfo)
        {
            var difference = (today - file.CreationTime).ToString("hh\\:mm");;
            Debug.Log(file.Name + " " + difference);
            Sprites.Add(LoadSprite(spritesPath + file.Name));
        }

        Image.sprite = Sprites.Last();
    }

    private Sprite LoadSprite(string filePath)
    {
        AssetDatabase.ImportAsset(filePath);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(filePath);

        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}