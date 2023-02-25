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

    public static async Task<Sprite> LoadSprite(IResourceLocation location)
    {
        var textureHandle  = Addressables.LoadAssetAsync<Texture2D>(location);
        var texture = await textureHandle.Task;
            
        return await CreateSprite(texture);
    }
    
    private static TaskCompletionSource<Texture2D> m_taskCompletionSource;
    private static string m_file;

    public static async Task<Sprite> LoadSprite(string file)
    {
        m_file = file;
        
        AssetDatabase.ImportAsset(m_file);
        Texture2D texture = await LoadTextureAsync();
        
        return await CreateSprite(texture);
    }

    private static async Task<Texture2D> LoadTextureAsync()
    {
        m_taskCompletionSource = new TaskCompletionSource<Texture2D>();
        
        EditorApplication.update += ImportAsset;  // import on the main thread
        
        Texture2D texture = await m_taskCompletionSource.Task;

        return texture;
    }

   

    private static void ImportAsset()
    {
        AssetDatabase.ImportAsset(m_file);
        
        Texture2D texture = AssetDatabase.LoadAssetAtPath<Texture2D>(m_file);
        
        m_taskCompletionSource.SetResult(texture);
        
        EditorApplication.update -= ImportAsset;
    }

    private static Task<Sprite> CreateSprite(Texture2D texture) => 
        Task.FromResult(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero));
}