using System.Collections.Generic;
using System.Threading.Tasks;

public interface ILoad
{ 
    Task<List<ItemData>> LoadItemsData();
}