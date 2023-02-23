using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILoad
{ 
    Task<List<Sprite>> Load();
}