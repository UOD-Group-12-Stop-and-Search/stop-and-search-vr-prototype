using System;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace Items
{
    public class NpcItemController : MonoBehaviour
    {

        [FormerlySerializedAs("knifeObject")]
        [SerializeField] private GameObject m_knifeObject = null!;
        [FormerlySerializedAs("drugObject")]
        [SerializeField] private GameObject m_drugObject = null!;
        [FormerlySerializedAs("itemObject")]
        [SerializeField] private GameObject m_itemObject = null!;

        [FormerlySerializedAs("itemGenerationChance")]
        [SerializeField] private int m_itemGenerationChance = 60;

        private static GameObject? CurrentSusItem { get; set; }
        public static GameObject? NpcWithSusItem { get; private set; }

        private readonly Random m_random = new Random();

        public void AttachItem(GameObject npc, string locationName)
        {
            Transform location = npc.transform.Find(locationName);
            int next;

            Debug.Log("NpcWithSusItem is null: " + (NpcWithSusItem == null));
            Debug.Log("NpcWithSusItem: " + NpcWithSusItem);

            if (NpcWithSusItem is not null)
            {
                next = m_random.Next(0, 100);

                Debug.Log("Triggered item generation");
                Debug.Log("Next: " + next);

                if (next <= m_itemGenerationChance)
                {
                    Instantiate(m_itemObject, location);
                }
                return;
            }

            int itemType = m_random.Next(0, 2);
            GameObject item = null;

            switch (itemType)
            {
                case 0:
                    item = m_knifeObject;
                    break;
                case 1:
                    item = m_drugObject;
                    break;
                case 2:
                    item = m_itemObject;
                    break;
            }
            Debug.Log("Triggered potential sus item generation");
            Debug.Log("Item type: " + itemType);
            Debug.Log("Item: " + item);

            next = m_random.Next(0, 4);

            if (item && next <= 3)
            {
                CurrentSusItem = Instantiate(item, location);

                if (item != m_itemObject)
                {
                    NpcWithSusItem = npc;
                }
            }

        }

        public void DropItem()
        {
            if (NpcWithSusItem == null)
            {
                return;
            }

            Destroy(CurrentSusItem);
            CurrentSusItem = null;
            NpcWithSusItem = null;
        }

        public bool ItemGenerated()
        {
            return CurrentSusItem != null;
        }

        public bool ItemFound(GameObject item)
        {
            return item == CurrentSusItem;
        }

    }
}