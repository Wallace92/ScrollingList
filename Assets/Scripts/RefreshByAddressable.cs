using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;


public class RefreshByAddressable : IRefresh
{
    private readonly ContentData m_contentData;

    public RefreshByAddressable(ContentData contentData) => m_contentData = contentData;

    public async Task<List<ItemData>> Refresh(ILoad loader)
    {
        AssetDatabase.SaveAssets();
        
        RefreshAddressableGroup();
        return await loader.LoadItemsData();
    }

    private void RefreshAddressableGroup()
    {
        var settings = AddressableAssetSettingsDefaultObject.Settings;
        
        AddressableAssetGroup group = settings.FindGroup(m_contentData.GroupName);
        
        var existingEntries = group.entries.ToArray();
        var fileInfo = LoaderHelper.GetFileInfo(m_contentData.Path);

        foreach (var file in fileInfo)
        {
            var pathToObject = m_contentData.Path + file.Name;
            
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
        var isLabelAssigned = existingEntries.Any(existingEntry => existingEntry.labels.Contains(m_contentData.Label));
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

        entry.labels.Add(m_contentData.Label);
        entry.address = pathToObject;

        return entry;
    }
}