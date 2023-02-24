using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public static class LoaderHelper
{
    public static Task<Sprite> CreateSprite(Texture2D texture) => 
        Task.FromResult(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero));
    
    
    public static FileInfo[] GetFileInfo(string directoryPath)
    {
        var info = new DirectoryInfo(directoryPath);

        return info.GetFiles()
            .Where(file => file.Extension != ".meta")
            .ToArray();
    }
}