using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;


public class RefreshByAddressable : IRefresh
{
    private string spritesPath;
    private string groupName;
    private string label;

    public RefreshByAddressable(string spritesPath, string groupName, string label)
    {
        this.spritesPath = spritesPath;
        this.groupName = groupName;
        this.label = label;
    }

    public async Task<List<ItemData>> Refresh(ILoad loader)
    {
        AssetDatabase.SaveAssets();
        
        RefreshAddressableGroup();
        return await loader.LoadItemsData();
    }

    private void RefreshAddressableGroup()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        
        AddressableAssetGroup group = settings.FindGroup(groupName);
        
        var existingEntries = group.entries.ToArray();
        var fileInfo = LoaderHelper.GetFileInfo(spritesPath);

        foreach (var file in fileInfo)
        {
            var pathToObject = spritesPath + file.Name;
            
            if (IsEntryExist(pathToObject, existingEntries))
                continue;
            
            var entry = AddEntry(pathToObject, group, settings);

            settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entry, true);
            AssetDatabase.SaveAssets();
        }
    }

    private bool IsEntryExist(string pathToObject, AddressableAssetEntry[] existingEntries)
    {
        var isOldObject = existingEntries.Any(existingEntry => existingEntry.address == pathToObject);
        var isLabelAssigned = existingEntries.Any(existingEntry => existingEntry.labels.Contains(label));
        return isOldObject && isLabelAssigned;
    }

    private AddressableAssetEntry AddEntry(string pathToObject, AddressableAssetGroup group, AddressableAssetSettings settings)
    {
        //important for guid empty possibility
        AssetDatabase.Refresh();
        
        var guid = AssetDatabase.AssetPathToGUID(pathToObject, AssetPathToGUIDOptions.OnlyExistingAssets);

        if (string.IsNullOrEmpty(guid))
            throw new ArgumentNullException("Guid cannot be null or empty.");
        
        var entry = settings.CreateOrMoveEntry(guid, group);

        entry.labels.Add(label);
        entry.address = pathToObject;

        return entry;
    }
}