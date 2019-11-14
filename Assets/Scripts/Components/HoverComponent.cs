using UnityEngine;

namespace Assets.Scripts.Components
{
    public class HoverComponent : MonoBehaviour
    {

        private Color mouseOverColor = Color.blue;

        private Color originalColor = Color.white;


        /// <summary>
        /// Translation alongside Z axis.
        /// </summary>
        public float TranslationZ = 1f;

        public float TranslationY = 1f;

        /// <summary>
        /// Scale factor to make the object looks bigger.
        /// </summary>
        public float ScaleFactor = 1.5f;

        public float HoverAnimationSpeed = 100f;

        private bool _isHovered = false;

        private Vector3 _originPosition;

        private Vector3 _hoverPosition;

        private bool _isInHoverPosition = false;

        // Start is called before the first frame update
        void Start()
        {
            _originPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isHovered)
            {
                if (!_isInHoverPosition)
                {
                    PlayHoverAnimation();
                }
            }
            else if (transform.position != _originPosition)
            {
                PlayUnhoverAnimation();
            }
        }

        private void PlayHoverAnimation()
        {
            Debug.Log($"Dest: {_hoverPosition.x},{_hoverPosition.y}, {_hoverPosition.z}");
            float step = HoverAnimationSpeed * Time.deltaTime;

            //Vector3.MoveTowards(transform.position, _hoverPosition, step);
            //if (Vector3.Distance(transform.position, _hoverPosition) < 0.001f)
            //{
            //    transform.position = _hoverPosition;
            //    _isInHoverPosition = true;
            //}
            transform.position = _hoverPosition;
            _isInHoverPosition = true;

        }

        private void PlayUnhoverAnimation()
        {
            float step = HoverAnimationSpeed * Time.deltaTime;

            //Vector3.MoveTowards(_hoverPosition, transform.position, step);
            //if (Vector3.Distance(transform.position, _hoverPosition) < 0.001f)
            //{
            //    transform.position = _originPosition;
            //    _isInHoverPosition = false;
            //}
            transform.position = _originPosition;
            _isInHoverPosition = false;
        }

        private void SetIsHovered(bool state)
        {
            _isHovered = state;
            GetComponent<Renderer>().material.color = _isHovered ? mouseOverColor : originalColor;
            if (!state)
            {
                _isInHoverPosition = false;
            }
        }


        void OnMouseEnter()
        {
            _hoverPosition = new Vector3(_originPosition.x, _originPosition.y + TranslationY, _originPosition.z + TranslationZ);
            SetIsHovered(true);
        }

        void OnMouseExit()
        {
            _hoverPosition = _originPosition;

            SetIsHovered(false);
        }

        void OnMouseDrag()
        {
            _isInHoverPosition = true;
        }
    }
}
