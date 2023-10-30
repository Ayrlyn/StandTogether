using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUI : Singleton<HandUI>
{
	#region serialiazable variables
	[SerializeField] List<CharacterSlot> hand;
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
	public void AssignToFirstAvailableSlot(Entity entity)
    {
        foreach (CharacterSlot slot in hand)
        {
			if(slot.Entity != null) { continue; }
			slot.ShowCharacter(entity);
			return;
        }
    }

	public void AssignToSlot(int slot, Entity entity)
    {
		hand[slot].ShowCharacter(entity);
    }

	public void ClearAll()
    {
        foreach (CharacterSlot slot in hand)
        {
			if(slot.Entity != null)
            {
				slot.ClearEntity();
            }
        }
    }

	public void HealAll()
    {
        foreach (CharacterSlot slot in hand)
        {
			if(slot.Entity != null)
            {
				slot.Entity.TakeDamage(-2);
				slot.SetHealth(slot.Entity.CurrentHealth);
            }
        }
    }
	#endregion

	#region coroutines
	#endregion
}
