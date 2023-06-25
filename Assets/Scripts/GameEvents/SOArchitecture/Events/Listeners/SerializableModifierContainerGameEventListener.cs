using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[AddComponentMenu(SOArchitecture_Utility.EVENT_LISTENER_SUBMENU + "SerializableModifierContainer")]
	public sealed class SerializableModifierContainerGameEventListener : BaseGameEventListener<SerializableModifierContainer, SerializableModifierContainerGameEvent, SerializableModifierContainerUnityEvent>
	{
	}
}