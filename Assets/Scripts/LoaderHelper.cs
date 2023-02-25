using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceLocations;

public static class LoaderHelper
{
    public static List<FileInfo> GetFileInfo(string directoryPath)
    {
        var info = new DirectoryInfo(directoryPath);

        return info.GetFiles()
            .Where(file => file.Extension != ".meta")
            .ToList();
    }

    public static async Task<Sprite> LoadSprite(string file)
    {
        AssetDatabase.ImportAsset(file);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D >(file);
        return await CreateSprite(texture);
    }

    public static async Task<Sprite> LoadSprite(IResourceLocation location)
    {
        var textureHandle  = Addressables.LoadAssetAsync<Texture2D>(location);
        var texture = await textureHandle.Task;
            
        return await CreateSprite(texture);
    }

    private static Task<Sprite> CreateSprite(Texture2D texture) => 
        Task.FromResult(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero));
}