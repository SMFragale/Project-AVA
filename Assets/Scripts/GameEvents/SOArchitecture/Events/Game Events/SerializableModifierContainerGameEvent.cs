using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	[CreateAssetMenu(
	    fileName = "SerializableModifierContainerGameEvent.asset",
	    menuName = SOArchitecture_Utility.GAME_EVENT + "Serializable Modifier Container",
	    order = 120)]
	public sealed class SerializableModifierContainerGameEvent : GameEventBase<SerializableModifierContainer>
	{
	}
}