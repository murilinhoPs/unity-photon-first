using UnityEngine;

namespace Utils
{
    public class LevelBounds : MonoBehaviour
    {
        [SerializeField] private MeshCollider spawnArea;
        private static float minBoundsX { get; set; }
        private static float maxBoundsX { get; set; }

        private static float minBoundsY { get; set; }
        private static float maxBoundsY { get; set; }


        private void Start()
        {
            var bounds = spawnArea.bounds;
            minBoundsX = bounds.min.x;
            maxBoundsX = bounds.max.x;
            minBoundsY = bounds.min.y;
            maxBoundsY = bounds.max.y;
        }

        public static float RandomPosX => Random.Range(minBoundsX, maxBoundsX);
        public static float RandomPosY => Random.Range(minBoundsY, maxBoundsY);
    }
}