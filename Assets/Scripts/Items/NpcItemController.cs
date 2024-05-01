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

        public GameObject? currentSusItem { get; private set; }
        public GameObject? npcWithSusItem { get; private set; }

        private readonly Random _random = new Random();

        private enum ItemTypes
        {
            Knife,
            Drug
        }

        public void AttachItem(GameObject? npc, String locationName)
        {
            Transform? location;
            if (npcWithSusItem)
            {
                int i = _random.Next(5);

                if (i < 2)
                {
                    location = npc.transform.Find(locationName);
                    if (location)
                    {
                        Instantiate(itemObject, location);
                    }
                }

                return;
            }

            if (_random.Next(5) < 2)
            {
                location = npc.transform.Find(locationName);
                Instantiate(itemObject, location);
                return;
            }

            int next = _random.Next(2);
            location = npc.transform.Find(locationName);
            currentSusItem = next switch
            {
                (int)ItemTypes.Knife => Instantiate(knifeObject, location),
                (int)ItemTypes.Drug => Instantiate(drugObject, location),
                _ => currentSusItem
            };
            npcWithSusItem = npc;
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