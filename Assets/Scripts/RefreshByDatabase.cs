using System.Collections.Generic;
using System.Threading.Tasks;

public class RefreshByDatabase : IRefresh
{
    public Task<List<ItemData>> Refresh(ILoad loader) => loader.LoadItemsData();
}