using TMPro;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private IObjectPool<Item> m_objectPool;
    public IObjectPool<Item> ObjectPool
    {
        set => m_objectPool = value;
    }

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