using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;

public class ScrollingListManager : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public Image Image;
    public ScrollRect ScrollRect;
    public GameObject ItemPrefab;
    public string spritesPath;
    public bool LoadUsingAddressables;

    private ILoad Loader => LoadUsingAddressables ? new LoadByAddressable() : new LoadByDatabase();

    private async void Start()
    {
        var item = GameObject.Instantiate(ItemPrefab, ScrollRect.content.transform);
        
        Sprites = await Loader.Load();
        
        Image.sprite = Sprites.Last();
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
    
    public void AddNewSprite()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        string groupName = "Content";
        string pathToObject = "Assets/Content/lab02.png";

        AddressableAssetGroup group = settings.FindGroup(groupName);

        var guid = AssetDatabase.AssetPathToGUID(pathToObject);
        var entry = settings.CreateOrMoveEntry(guid, group);

        entry.labels.Add("item");
        entry.address = pathToObject;

        //Save the changes!
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);

        AssetDatabase.SaveAssets();
    }
    
         private Sprite LoadSprite(string filePath)
     {
         AssetDatabase.ImportAsset(filePath);
         
         Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(filePath);

         return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
     }
}