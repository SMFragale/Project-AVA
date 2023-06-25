using UnityEngine;

namespace ScriptableObjectArchitecture
{
	[System.Serializable]
	public sealed class SerializableModifierContainerReference : BaseReference<SerializableModifierContainer, SerializableModifierContainerVariable>
	{
	    public SerializableModifierContainerReference() : base() { }
	    public SerializableModifierContainerReference(SerializableModifierContainer value) : base(value) { }
	}
}