﻿using System;
using UnityEngine;

//TODO: Refactor this to use StateMachine.
namespace Unity.Components
{
    /// <summary>
    /// https://forum.unity.com/threads/implement-a-drag-and-drop-script-with-c.130515/
    /// </summary>
    public class DraggingComponent : MonoBehaviour
    {
        private bool _isDragging;

        private bool _isGoingBack;

        private float distance;

        private float _lockedY;

        private Vector3 _screenPoint;

        private Vector3 _originPosition;

        public float SpeedBackToOrigin = 10f;

        private Vector3 _screenSpace;

        private Vector3 _offset;

        private Camera _mainCamera;

        public float DragDistanceForward = -25f;
        
        public float DragDistanceBackward = 25f;

        public event EventHandler OnDragForwardSuccessful;
        public event EventHandler OnDragBackwardSuccessful;

        // Start is called before the first frame update
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_isGoingBack)
            {
                GoBackToOrigin();
            }
        }

        public void Drag()
        {
            //keep track of the mouse position
            Vector3 cursorScreenSpace = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenSpace.z);

            //convert the screen mouse position to world point and adjust with offset
            Vector3 curPosition = _mainCamera.ScreenToWorldPoint(cursorScreenSpace) /*+ _offset*/;

            //update the position of the object in the world
            Transform transform1 = transform;
            transform1.position = curPosition;
            float currentDistance = _originPosition.z - transform1.position.z;
//            Debug.Log(currentDistance);
            if (currentDistance < DragDistanceForward)
            {
                OnDragForwardSuccessful?.Invoke(this, null);
                OnDragForwardSuccessful = (sender, args) => { };
            } else if (currentDistance > DragDistanceBackward)
            {
                OnDragBackwardSuccessful?.Invoke(this, null);
                OnDragBackwardSuccessful = (sender, args) => { };
            }
        }

        private void GoBackToOrigin()
        {
            float step = SpeedBackToOrigin * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _originPosition, step);
            if (Vector3.Distance(transform.position, _originPosition) < 0.001f)
            {
                transform.position = _originPosition;
                _isGoingBack = false;
            }
        }

        private void SetDraggingState(bool isDragging)
        {
            _isDragging = isDragging;
            _isGoingBack = !isDragging;
        }

        public void StartDragging()
        {
            if (_mainCamera == null) return;
            Transform objectTransform = transform;
            _originPosition = objectTransform.position;
            //translate the objects position from the world to Screen Point
            _screenSpace = _mainCamera.WorldToScreenPoint(_originPosition);

            //calculate any difference between the objects world position and the mouses Screen position converted to a world point  
            _offset = transform.position -
                      _mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                          _screenSpace.z));
            SetDraggingState(true);
        }

        public void StopDragging()
        {
            SetDraggingState(false);
        }
    }
}