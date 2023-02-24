using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILoad
{ 
    Task<List<ItemData>> LoadItemsData();
}

public interface IRefresh
{
    Task<List<ItemData>> Refresh(ILoad loader);
}