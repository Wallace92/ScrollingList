using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public TextMeshProUGUI NameTMP;
    public TextMeshProUGUI TimeTMP;
    public Image Image;

    public void AssignData(ItemData itemData)
    {
        NameTMP.text = itemData.Name;
        TimeTMP.text = itemData.Time;
        Image.sprite = itemData.Image;
    }
}