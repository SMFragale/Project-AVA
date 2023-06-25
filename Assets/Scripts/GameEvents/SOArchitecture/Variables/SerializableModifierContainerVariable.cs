using UnityEngine;
using UnityEngine.Events;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public class SerializableModifierContainerEvent : UnityEvent<SerializableModifierContainer> { }

	[CreateAssetMenu(
	    fileName = "SerializableModifierContainerVariable.asset",
	    menuName = SOArchitecture_Utility.VARIABLE_SUBMENU + "Serializable Modifier Container",
	    order = 120)]
	public class SerializableModifierContainerVariable : BaseVariable<SerializableModifierContainer, SerializableModifierContainerEvent>
	{
	}
}