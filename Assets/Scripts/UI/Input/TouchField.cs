using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using System;

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
        public Vector2 PointerOld;
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

        [Range(0.25f, 1.0f)]
        [SerializeField]
        private float m_FieldScreenCoverPtg = 0.75f;

        // Use this for initialization
        void Awake()
        {
            Rect rect = GetComponent<RectTransform>().rect;
            rect.xMin = Screen.width*(1-m_FieldScreenCoverPtg);
            GetComponent<RectTransform>().rect.Set(rect.xMin, rect.yMin, rect.width, rect.height);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0 && PointerId >= 0 && PointerId < Input.touches.Length)
            {
                Touch touch = Input.GetTouch(PointerId);
                
                
                switch(touch.phase)
                {
                    case TouchPhase.Began:
                        PointerOld = touch.position;
                        TouchDist = new Vector2();
                        break;
                    case TouchPhase.Moved:
                    case TouchPhase.Stationary:
                        TouchDist = touch.position - PointerOld;
                        PointerOld = touch.position;
                        break;
                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        TouchDist = new Vector2();
                        break;
                }
                SendValueToControl(TouchDist);
            }
            // if (Pressed)
            // {
                
            //     if (PointerId >= 0 && PointerId < Input.touches.Length)
            //     {
            //         TouchDist = Input.touches[PointerId].position - PointerOld;
            //         PointerOld = Input.touches[PointerId].position;
            //     }
            //     else
            //     {
            //         TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
            //         PointerOld = Input.mousePosition;
            //     }
            // }
            // else
            // {
            //     TouchDist = new Vector2();
            // }
        }

        /// <summary>
        /// Handles the event when the pointer is down
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            PointerId = eventData.pointerId;
            PointerOld = eventData.position;
        }

        /// <summary>
        /// Handles the event when the pointer is up
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
            PointerId = -1;
        }

        #if UNITY_EDITOR
            [CustomEditor(typeof(TouchField))]
            internal class TouchFieldEditor : UnityEditor.Editor
            {

                private SerializedProperty m_ControlPathInternal;
                private SerializedProperty m_FieldScreenCoverPtg;

                public void OnEnable()
                {
                    m_ControlPathInternal = serializedObject.FindProperty(nameof(TouchField.m_ControlPath));
                    m_FieldScreenCoverPtg = serializedObject.FindProperty(nameof(TouchField.m_FieldScreenCoverPtg));
                }

                public override void OnInspectorGUI()
                {
                    
                    float minValue = 0.25f;
                    float maxValue = 1.0f;

                    EditorGUILayout.Slider(m_FieldScreenCoverPtg, minValue, maxValue, new GUIContent("Screen Cover Ptg"));
                    EditorGUILayout.PropertyField(m_ControlPathInternal);

                    serializedObject.ApplyModifiedProperties();
                }
            }
        #endif
    }
    
}