using UnityEngine;
using UnityEngine.UI;

public class ItemData
{
    public ItemData(string name, string time, Sprite image)
    {
        Name = name;
        Time = time;
        Image = image;
    }

    public string Name;
    public string Time;
    public Sprite Image;
}