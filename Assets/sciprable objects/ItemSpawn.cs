using System.Collections.Generic;
using UnityEngine;

public enum ItemType { Head, SpaceStone, Hat }

public class ItemSpawn : MonoBehaviour
{

}
//{
//    List<Item> itemInventory = new List<Item>();
//    public ItemSO[] itemDB;

//    public void ItemCreate(ItemType inputWeaponType)
//    {
//        int itemIndex = (int)inputWeaponType;

//        GameObject itemGO = new GameObject(inputWeaponType.ToString());
//        itemGO.transform.parent = this.gameObject.transform;

//        Item item = itemGO.AddComponent<Item>();

//        item.itemName = itemDB[itemIndex].itemName;
//        item.price = itemDB[itemIndex].price;
//        item.weight = itemDB[itemIndex].weight;
//        item.PreFab = itemDB[itemIndex].PreFab;

//        itemInventory.Add(item);
//    }

//    void Start()
//    {
//        ItemCreate(ItemType.Hat);
//        ItemCreate(ItemType.Head);
//        ItemCreate(ItemType.SpaceStone);

//        ShowInventoryItems();
//    }

//    void ShowInventoryItems()
//    {
//        foreach (Item i in itemInventory)
//        {
//            Debug.Log(i.itemName + " price: " + i.price);
//        }
//    }
//}
