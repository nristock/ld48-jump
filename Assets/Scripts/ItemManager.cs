using UnityEngine;
using System.Collections;

public class ItemManager : MonoBehaviour
{
    public GameObject itemPrefab;
    public GameObject moneyPrefab;

    public CollectableItem[] itemTypes;

    private void Start()
    {
        WorldManager.ItemManager = this;
    }

    public GameObject makeRandomItem(Vector3 location)
    {
        CollectableItem itemType = itemTypes[Random.Range(0, itemTypes.Length - 1)];

        var item = (GameObject)Instantiate(itemPrefab);
        item.transform.position = location;
        item.renderer.material = itemType.itemMaterial;
        item.AddComponent(itemType.implementingBehaviourClassName);

        return item;
    }

    public GameObject makeMoney(Vector3 location)
    {
        var money = (GameObject)Instantiate(moneyPrefab);
        money.transform.position = location;

        return money;
    }
}
