using UnityEngine;

namespace NSTools.Utils
{
    public class ZSetDDOU : MonoBehaviour
    {
        void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

    }
}
