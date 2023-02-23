using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadByAddressable : ILoad
{
    public async Task<List<Sprite>> Load()
    {
        var handle = Addressables.LoadAssetsAsync<Texture2D>("item", null);
        var _ = await handle.Task;

        if (handle.Status != AsyncOperationStatus.Succeeded)
        {
            Debug.LogWarning("Some assets did not load.");
            return null;
        }

        var sprites = new List<Sprite>();
        foreach (Texture2D texture in handle.Result)
        {
            sprites.Add(await LoaderHelper.CreateSprite(texture));
        }

        return sprites;
    }

}