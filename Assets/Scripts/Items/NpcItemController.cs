using System;
using UnityEngine;
using Random = System.Random;

namespace Items
{
    public class NpcItemController : MonoBehaviour
    {

        [SerializeField] private GameObject? knifeObject;
        [SerializeField] private GameObject? drugObject;
        [SerializeField] private GameObject? itemObject;

        [SerializeField] private int itemGenerationChance = 60;

        private static GameObject? currentSusItem { get; set; }
        private static GameObject? npcWithSusItem { get; set; }

        private readonly Random _random = new Random();

        public void AttachItem(GameObject? npc, String locationName)
        {
            Transform location = npc.transform.Find(locationName);
            int next;

            Debug.Log("NpcWithSusItem is null: " + (npcWithSusItem == null));
            Debug.Log("NpcWithSusItem: " + npcWithSusItem);

            if (npcWithSusItem is not null)
            {
                next = _random.Next(0, 100);

                Debug.Log("Triggered item generation");
                Debug.Log("Next: " + next);

                if (next <= itemGenerationChance)
                {
                    Instantiate(itemObject, location);
                }
                return;
            }

            int itemType = _random.Next(0, 2);
            GameObject item = null;

            switch (itemType)
            {
                case 0:
                    item = knifeObject;
                    break;
                case 1:
                    item = drugObject;
                    break;
                case 2:
                    item = itemObject;
                    break;
            }
            Debug.Log("Triggered potential sus item generation");
            Debug.Log("Item type: " + itemType);
            Debug.Log("Item: " + item);

            next = _random.Next(0, 4);

            if (item && next <= 3)
            {
                currentSusItem = Instantiate(item, location);

                if (item != itemObject)
                {
                    npcWithSusItem = npc;
                }
            }

        }

        public void DropItem()
        {
            if (npcWithSusItem == null)
            {
                return;
            }

            Destroy(currentSusItem);
            currentSusItem = null;
            npcWithSusItem = null;
        }

        public bool ItemGenerated()
        {
            return currentSusItem != null;
        }

        public bool ItemFound(GameObject item)
        {
            return item == currentSusItem;
        }

    }
}