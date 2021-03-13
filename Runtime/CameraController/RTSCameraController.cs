using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Kellojo.CameraController {
    

    public class RTSCameraController : MonoBehaviour {
        [HideInInspector] public CinemachineVirtualCamera RTSCamera;
        public Transform CameraTransform;

        public float NormalSpeed = 5;
        public float FastSpeed = 5;
        public float MovementTime = 5;
        private float MovementSpeed;
        public bool EnableMousePaning = true;

        public float RotationAmount = 5f;
        public float RotationTime = 5;

        public float minY = 0;
        public float maxY = 15;
        public Vector3 ZoomAmount = new Vector3(0, -5, 5);
        public bool EnableKeyboardZoom = false;


        private Vector3 newPosition;
        private Quaternion newRotation;
        private Vector3 newZoom;
        private Vector3 dragStartPosition;
        private Vector3 dragCurrentPosition;
        private Vector3 rotateStartPosition;
        private Vector3 rotateCurrentPosition;
        private Camera _camera;

        private new Camera camera {
            get {
                if (_camera == null) {
                    _camera = Camera.main;
                }

                return _camera;
            }
        }


        void Awake() {
            RTSCamera = CameraTransform.GetComponent<CinemachineVirtualCamera>();
        }
        void Start() {
            newPosition = transform.position;
            newRotation = transform.rotation;
            newZoom = CameraTransform.localPosition;
        }
        void Update() {
            HandleMovementInput();
            HandleMouseInput();
        }

        void HandleMouseInput() {

            // dragging via mouse
            if (Input.GetMouseButtonDown(0) && EnableMousePaning) {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float entry;

                if (plane.Raycast(ray, out entry)) {
                    dragStartPosition = ray.GetPoint(entry);
                }
            }
            if (Input.GetMouseButton(0) && EnableMousePaning) {
                Plane plane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                float entry;

                if (plane.Raycast(ray, out entry)) {
                    dragCurrentPosition = ray.GetPoint(entry);
                    newPosition = transform.position + dragStartPosition - dragCurrentPosition;
                }
            }

            // zooming
            if (Input.mouseScrollDelta.y != 0) {
                newZoom += Input.mouseScrollDelta.y * ZoomAmount;
            }

            if (Input.GetMouseButtonDown(2)) {
                rotateStartPosition = Input.mousePosition;
            }
            if (Input.GetMouseButton(2)) {
                rotateCurrentPosition = Input.mousePosition;
                Vector3 difference = rotateStartPosition - rotateCurrentPosition;
                rotateStartPosition = rotateCurrentPosition;
                newRotation *= Quaternion.Euler(Vector3.up * (-difference.x / 5f));
            }
        }
        void HandleMovementInput() {
            if (Input.GetKey(KeyCode.LeftShift)) {
                MovementSpeed = FastSpeed;
            } else {
                MovementSpeed = NormalSpeed;
            }


            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
                newPosition += transform.forward * MovementSpeed;
            }
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
                newPosition += -transform.forward * MovementSpeed;
            }
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
                newPosition += -transform.right * MovementSpeed;
            }
            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
                newPosition += transform.right * MovementSpeed;
            }

            if (Input.GetKey(KeyCode.Q)) {
                newRotation *= Quaternion.Euler(Vector3.up * RotationAmount);
            }
            if (Input.GetKey(KeyCode.E)) {
                newRotation *= Quaternion.Euler(Vector3.up * -RotationAmount);
            }

            if (Input.GetKey(KeyCode.R) && EnableKeyboardZoom) {
                newZoom += ZoomAmount;
            }
            if (Input.GetKey(KeyCode.F) && EnableKeyboardZoom) {
                newZoom -= ZoomAmount;
            }

            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * MovementTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, Time.deltaTime * RotationTime);
            newZoom.y = Mathf.Clamp(newZoom.y, minY, maxY);
            newZoom.z = Mathf.Clamp(newZoom.z, -maxY, -minY);
            CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, newZoom, Time.deltaTime * MovementTime);
        }

        /// <summary>
        /// Move to this position smoothly
        /// </summary>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        public void MoveToPosition(Vector3 position, Vector3 rotation) {
            newPosition = position;
            newRotation = Quaternion.Euler(rotation);
        }
    }

}

