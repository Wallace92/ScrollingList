using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.VersionControl;

public class RefreshByAddressable : IRefresh
{
    private string spritesPath = "Assets/Content/";
    public async Task<List<ItemData>> Refresh(ILoad loader)
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;

        string groupName = "Content";
        
        AddressableAssetGroup group = settings.FindGroup(groupName);
        
        var existingEntries = group.entries.ToArray();
        var fileInfo = LoaderHelper.GetFileInfo(spritesPath);

        foreach (var file in fileInfo)
        {
            // "Assets/Content/lab02.png" - extension!
            
            if (existingEntries.Any(existingEntry => existingEntry.address == spritesPath + file.Name) &&  existingEntries.Any(existingEntry => existingEntry.labels.Contains("item")))
                continue;
            
            var entry = AddEntry(spritesPath + file.Name, group, settings);

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();
        }
        
        AssetDatabase.Refresh();

        return await loader.LoadItemsData();
    }

    private AddressableAssetEntry AddEntry(string pathToObject, 
        AddressableAssetGroup group, AddressableAssetSettings settings)
    {
        var guid = AssetDatabase.AssetPathToGUID(pathToObject);
        var entry = settings.CreateOrMoveEntry(guid, group);

        entry.labels.Add("item");
        entry.address = pathToObject;

        return entry;
    }
}