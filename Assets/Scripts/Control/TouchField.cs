using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

#if UNITY_EDITOR
using UnityEditor;
#endif
namespace AVA.Control
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

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Pressed)
            {
                if (PointerId >= 0 && PointerId < Input.touches.Length)
                {
                    TouchDist = Input.touches[PointerId].position - PointerOld;
                    PointerOld = Input.touches[PointerId].position;
                }
                else
                {
                    TouchDist = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - PointerOld;
                    PointerOld = Input.mousePosition;
                }
            }
            else
            {
                TouchDist = new Vector2();
            }
            SendValueToControl(TouchDist);
        }

        /// <summary>
        /// Handles the event when the pointer is down
        /// </summary>
        public void OnPointerDown(PointerEventData eventData)
        {
            Pressed = true;
            PointerId = eventData.pointerId;
            PointerOld = eventData.position;
        }

        /// <summary>
        /// Handles the event when the pointer is up
        /// </summary>
        public void OnPointerUp(PointerEventData eventData)
        {
            Pressed = false;
        }

        #if UNITY_EDITOR
            [CustomEditor(typeof(TouchField))]
            internal class TouchFieldEditor : UnityEditor.Editor
            {

                private SerializedProperty m_ControlPathInternal;

                public void OnEnable()
                {
                    m_ControlPathInternal = serializedObject.FindProperty(nameof(TouchField.m_ControlPath));
                }

                public override void OnInspectorGUI()
                {
                    EditorGUILayout.PropertyField(m_ControlPathInternal);

                    serializedObject.ApplyModifiedProperties();
                }
            }
        #endif
    }
    
}