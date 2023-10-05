using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using System;
using System.Net;
using System.ComponentModel;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace AVA.UI
{
    /// <summary>
    /// Class that handles the touch input of the player inside the gameObject's rect transform as a Vector2
    /// Binds the input as a Vector2 to the control path specified in the inspector
    /// </summary>
    public class TouchField : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        
        [HideInInspector]
        public Vector2 TouchDist;
        [HideInInspector]
        public Dictionary<int,Vector2> PointersOld;
        [HideInInspector]
        protected int PointerId;
        [HideInInspector]
        public bool Pressed;
        
        
        
        [InputControl(layout = "Vector2")]
        [SerializeField]
        private string m_ControlPath;
        protected override string controlPathInternal
        {
            get => m_ControlPath;
            set => m_ControlPath = value;
        }

        [Space, Header("TouchField Size"), Tooltip("Left border has to be < Right Border")]
        [Range(0.25f, 1.0f)]
        [SerializeField]
        private float m_LeftBoundaryPtg = 0.25f;

        [Range(0.25f, 1.0f)]
        [SerializeField, Tooltip("Right Border has to be > Left Border")]
        private float m_RightBoundaryPtg = 1.0f;

        // Use this for initialization
        void Awake()
        {
            RectTransform rectTransform = GetComponent<RectTransform>();

            // Set the size of the RectTransform to cover 75% of the screen horizontally and 100% vertically
            rectTransform.anchorMin = new Vector2(m_LeftBoundaryPtg, 0f);
            rectTransform.anchorMax = new Vector2(m_RightBoundaryPtg, 1);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            PointersOld = new Dictionary<int,Vector2>();
            PointerId = -1;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && PointerId >= 0 && PointerId < Input.touches.Length)
            {
                Touch touch = Input.GetTouch(PointerId);


                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        PointersOld[PointerId] = touch.position;
                        TouchDist = new Vector2();
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        TouchDist = touch.position - PointersOld[PointerId];
                        PointersOld[PointerId] = touch.position;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        TouchDist = new Vector2();
                        break;
                }
                SendValueToControl(TouchDist);
                //Debug.LogFormat("PointerId: {0}, position: {1}, positionOld: {2}, length: {3}, touchCount:{4}", PointerId, touch.position, PointersOld[PointerId], Input.touches.Length, Input.touchCount);
            }
        }

        /// <summary>
        /// Handles the event when the pointer is down
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            if (PointerId != -1) return;
            PointerId = eventData.pointerId;
            PointersOld[PointerId] = eventData.position;
        }

        /// <summary>
        /// Handles the event when the pointer is up
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            if(eventData.pointerId != PointerId) return;
            Pressed = false;
            PointersOld[PointerId] = eventData.position;
            PointerId = -1;
        }

        #if UNITY_EDITOR
            [CustomEditor(typeof(TouchField))]
            internal class TouchFieldEditor : UnityEditor.Editor
            {

                private SerializedProperty m_ControlPathInternal;
                private SerializedProperty m_LeftBoundaryPtg;
                private SerializedProperty m_RightBoundaryPtg;

                public void OnEnable()
                {
                    m_ControlPathInternal = serializedObject.FindProperty(nameof(TouchField.m_ControlPath));
                    m_LeftBoundaryPtg = serializedObject.FindProperty(nameof(TouchField.m_LeftBoundaryPtg));
                    m_RightBoundaryPtg = serializedObject.FindProperty(nameof(TouchField.m_RightBoundaryPtg));
                }

                public override void OnInspectorGUI()
                {
                    
                    float minValue = 0.25f;
                    float maxValue = 1.0f;

                    EditorGUILayout.Slider(m_LeftBoundaryPtg, minValue, maxValue, new GUIContent("Left Screen Border"));
                    EditorGUILayout.Slider(m_RightBoundaryPtg, minValue, maxValue, new GUIContent("Right Screen Border"));
                    EditorGUILayout.PropertyField(m_ControlPathInternal);

                    serializedObject.ApplyModifiedProperties();
                }
            }
        #endif
    }
    
}