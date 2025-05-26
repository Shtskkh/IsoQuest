using UnityEngine;

namespace Level_1
{
    public enum PlatformPosition
    {
        Left,
        Middle,
        Right
    }

    public class PlatformManager : MonoBehaviour
    {
        [SerializeField] private GameObject platformA;
        [SerializeField] private GameObject platformB;
        [SerializeField] private GameObject platformC;
        
        private void Awake()
        {
            MovePlatform(platformA, PlatformPosition.Right);
            MovePlatform(platformB, PlatformPosition.Middle);
            MovePlatform(platformC, PlatformPosition.Left);
        }

        public static void MoveToNextPosition(GameObject platform)
        {
            var position = platform.transform.localPosition.z;
            
            if (Mathf.Approximately(position, GetPlatformPosition(PlatformPosition.Left).z))
                MovePlatform(platform, PlatformPosition.Middle);
            else if (Mathf.Approximately(position, GetPlatformPosition(PlatformPosition.Middle).z))
                MovePlatform(platform, PlatformPosition.Right);
            else if (Mathf.Approximately(position, GetPlatformPosition(PlatformPosition.Right).z))
                MovePlatform(platform, PlatformPosition.Left);
        }

        public static void MovePlatform(GameObject platform, PlatformPosition position)
        {
            var newPosition = new Vector3(
                platform.transform.localPosition.x,
                platform.transform.localPosition.y,
                GetPlatformPosition(position).z);
            platform.transform.localPosition = newPosition;
        }

        private static Vector3 GetPlatformPosition(PlatformPosition platformPosition)
        {
            switch (platformPosition)
            {
                case PlatformPosition.Left:
                    return new Vector3(0f, 0f, 5f);
                case PlatformPosition.Middle:
                    return new Vector3(0f, 0f, 0f);
                case PlatformPosition.Right:
                    return new Vector3(0f, 0f, -5f);
                default:
                    Debug.LogError("Unknown platform position!");
                    return Vector3.zero;
            }
        }
    }
}