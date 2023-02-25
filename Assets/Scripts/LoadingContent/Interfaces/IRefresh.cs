using System.Collections.Generic;
using System.Threading.Tasks;

public interface IRefresh
{
    Task<List<ItemData>> Refresh(ILoad loader);
}