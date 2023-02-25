using UnityEngine;
using UnityEngine.Pool;

public class ItemPool : MonoBehaviour
{
    public Item Item;
    public Transform ContentTransform;
    public IObjectPool<Item> ItemsPool;

    [SerializeField]
    private bool arrowsPoolCheck;

    [SerializeField] 
    private int arrowsPoolCapacity;

    [SerializeField] 
    private int maxPoolSize;

    private void Awake() => ItemsPool = new ObjectPool<Item>
    (
        CreateArrows, 
        OnGetFromPool, 
        OnReleaseToPool, 
        OnDestroyPooledObject,
        arrowsPoolCheck, 
        arrowsPoolCapacity, 
        maxPoolSize
    );
    
    private Item CreateArrows()
    {
        Item item = Instantiate(Item, ContentTransform, true);
        item.ObjectPool = ItemsPool;
        return item;
    }
    
    private void OnReleaseToPool(Item pooledObject) => pooledObject.gameObject.SetActive(false);

    private void OnGetFromPool(Item pooledObject) => pooledObject.gameObject.SetActive(true);
    
    private void OnDestroyPooledObject(Item pooledObject) => Destroy(pooledObject.gameObject);
}