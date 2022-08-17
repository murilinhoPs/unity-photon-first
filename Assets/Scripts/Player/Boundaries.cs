using UnityEngine;

namespace Player
{
    public class Boundaries : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sprite;
        [SerializeField] private float minHeightOffset, maxHeightOffset, minWidthOffset, maxWidthOffset;

        private Vector2 _screenBounds;
        private Vector2 _positionBounds;
        private float _width;
        private float _height;
        private float minX, minY, maxX, maxY;

        private void Start()
        {
            _screenBounds =
                Camera.main!.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            var spriteSize = sprite.bounds.extents;
            _width = spriteSize.x;
            _height = spriteSize.y;
        
            minX = _screenBounds.x * -1 + _width - minWidthOffset;
            maxX = _screenBounds.x - _width + maxWidthOffset;
            minY = _screenBounds.y * -1 + _height - minHeightOffset;
            maxY = _screenBounds.y - _height + maxHeightOffset;

        }

        private void LateUpdate()
        {
            _positionBounds = transform.position;
            PositionWrap();
        }

        private void PositionWrap()
        {
            if (_positionBounds.x < minX)
            {
                _positionBounds = new Vector2(maxX, transform.position.y);
            }

            if (_positionBounds.x > maxX)
            {
                _positionBounds = new Vector2(minX, transform.position.y);
            }

            if (_positionBounds.y < minY)
            {
                _positionBounds = new Vector2(transform.position.x, maxY);
            }

            if (_positionBounds.y > maxY)
            {
                _positionBounds = new Vector2(transform.position.x, minY);
            }

            transform.position = _positionBounds;
        }


        private void PositionConstraints()
        {
            _positionBounds.x = Mathf.Clamp(_positionBounds.x, minX, maxX);
            _positionBounds.y = Mathf.Clamp(_positionBounds.y, minY, maxY);
            transform.position = _positionBounds;
        }
    }
}