using UnityEngine;

namespace Rosambo.Systems
{
    public class RosamboApplication : MonoBehaviour
    {
        [field: SerializeField] public GameSystem GameSystem { get; private set; }

        public static RosamboApplication Instance { get; private set; }
        public static ServiceLocator ServiceLocator { get; private set; }


        private void Awake()
        {
            Instance = this;
            ServiceLocator = new ServiceLocator();
        }
    }
}