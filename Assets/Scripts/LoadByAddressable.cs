using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadByAddressable : ILoad
{
    public async Task<List<Sprite>> Load()
    {
        var tcs = new TaskCompletionSource<List<Sprite>>();

        var handle = Addressables.LoadAssetsAsync<Texture2D>("item", null);
        handle.Completed += operation =>
        {
            if (operation.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogWarning("Some assets did not load.");
                tcs.SetResult(null);
                return;
            }

            var sprites = new List<Sprite>();
            foreach (Texture2D texture in operation.Result)
            {
                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
                sprites.Add(sprite);
            }

            tcs.SetResult(sprites);
        };

        return await tcs.Task;
    }
}