using System.Threading.Tasks;
using UnityEngine;

public static class LoaderHelper
{
    public static Task<Sprite> CreateSprite(Texture2D texture) => 
        Task.FromResult(Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero));
}