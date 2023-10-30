using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCharacterScreen : Singleton<NewCharacterScreen>
{
	#region serialiazable variables
	[SerializeField] NewCharacterSlot newCharacterSlot1;
	[SerializeField] NewCharacterSlot newCharacterSlot2;
	[SerializeField] ConfirmNewCharacterSlot confirmNewCharacter;
	#endregion

	#region local variables
	#endregion

	#region getters and setters
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void SetCharacterChoices(Entity entity1, Entity entity2)
    {
		this.gameObject.SetActive(true);
		newCharacterSlot1.Reset();
		newCharacterSlot1.ShowCharacter(entity1);
		newCharacterSlot2.Reset();
		newCharacterSlot2.ShowCharacter(entity2);

		newCharacterSlot1.MarkHighlightActive();
		confirmNewCharacter.Reset();
		SetChosenCharacter(null);
    }

	public void SetChosenCharacter(Entity entity)
    {
		confirmNewCharacter.SetEntity(entity);
    }
	#endregion

	#region coroutines
	#endregion
}
