using UnityEngine;

namespace Items
{
    public class ItemHelpers : MonoBehaviour
    {
        public void DetatchFromParent()
        {
            transform.SetParent(null);
        }
    }
}