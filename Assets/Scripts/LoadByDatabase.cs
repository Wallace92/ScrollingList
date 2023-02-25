using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LoadByDatabase : ILoad
{
    private readonly string m_spritesPath;

    public LoadByDatabase(string spritesPath) => m_spritesPath = spritesPath;

    public async Task<List<ItemData>> LoadItemsData()
    {
        var itemsData = new List<ItemData>();
        var fileInfo =  LoaderHelper.GetFileInfo(m_spritesPath);
        
        DateTime today = DateTime.Now;

        foreach (var file in fileInfo)
        {
            var difference = (today - file.CreationTime).ToString("dd\\:hh\\:mm");
            var sprite = await LoaderHelper.LoadSprite(m_spritesPath + file.Name);
            var itemData = new ItemData(file.Name, difference, sprite);
            
            itemsData.Add(itemData);
            
            LoadingProgress.SetProgressBarValues(fileInfo.Count, fileInfo.IndexOf(file) + 1);
        }

        return itemsData;
    }
}