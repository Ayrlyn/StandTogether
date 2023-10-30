using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmNewCharacterSlot : Slot
{
	#region serialiazable variables
	[SerializeField] Image confirmActive;
	[SerializeField] Image confirmInactive;
	#endregion

	#region local variables
    #endregion

    #region getters and setters
    #endregion

    #region unity methods
    #endregion

    #region local methods
    public override void OnButtonPressed(Button button)
    {
        if (!this.gameObject.activeInHierarchy) { return; }
        base.OnButtonPressed(button);
        if(button == Button.Select && IsHighlighted)
        {
            if (Entity != null)
            {
                SceneReferences.NewCharacterScreen.gameObject.SetActive(false);
                SceneReferences.HandUI.gameObject.SetActive(true);
                SceneReferences.FieldUI.gameObject.SetActive(true);
                SceneReferences.Game.NextRound(Entity);
            }
        }
    }
    #endregion

    #region public methods
    public void SetEntity(Entity entity)
    {
		Entity = entity;
		confirmActive.gameObject.SetActive(entity != null);
		confirmInactive.gameObject.SetActive(entity == null);
    }
	#endregion

	#region coroutines
	#endregion
}
