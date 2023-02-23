using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Item(string name, string time, Image image)
    {
        Name = name;
        Time = time;
        Image = image;
    }

    public string Name;
    public string Time;
    public Image Image;
}