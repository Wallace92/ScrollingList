using UnityEngine;

public class ItemData
{
    public ItemData(string name, string time, Sprite image)
    {
        Name = name;
        Time = time;
        Image = image;
    }

    public readonly string Name;
    public readonly string Time;
    public readonly Sprite Image;
}