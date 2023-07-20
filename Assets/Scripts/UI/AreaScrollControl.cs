
using UnityEngine;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Layouts;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AVA.Control
{
    public class AreaScrollControl : OnScreenControl
{
    private Touch touch;

    private int rightSideTouchFingerId;
    
    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
            {
                get => m_ControlPath;
                set => m_ControlPath = value;
            }


    //start
    private void Start()
    {
        touch = new Touch();
        touch.phase = TouchPhase.Ended;
        rightSideTouchFingerId = -1;
    }

    //update
    private void Update()
    {
        if (Input.touchCount > 0)
        {
            
            for(int i = 0; i < Input.touchCount; i++)
            {
                Touch touchTemp = Input.GetTouch(i);
                if (touchTemp.position.x > Screen.width / 2 )
                {
                    touch = Input.GetTouch(i);

                    if(touch.phase == TouchPhase.Began)
                    {
                        rightSideTouchFingerId = touch.fingerId;
                    }
                    else if(touch.phase == TouchPhase.Ended)
                    {
                        SendValueToControl(new Vector2(0, 0));
                        rightSideTouchFingerId = -1;
                    }
                }
            }
        }

        if(rightSideTouchFingerId >= 0 && rightSideTouchFingerId < Input.touchCount)
        {
            Debug.Log("VAlue Sent: " + Input.GetTouch(rightSideTouchFingerId).deltaPosition);
            SendValueToControl(Input.GetTouch(rightSideTouchFingerId).deltaPosition);
        }
    }

    #if UNITY_EDITOR
        [CustomEditor(typeof(OnScreenStick))]
        internal class OnScreenStickEditor : UnityEditor.Editor
        {

            private SerializedProperty m_ControlPathInternal;

            public void OnEnable()
            {
                m_ControlPathInternal = serializedObject.FindProperty(nameof(AreaScrollControl.m_ControlPath));
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
