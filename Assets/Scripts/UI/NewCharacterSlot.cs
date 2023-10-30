using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCharacterSlot : Slot
{
	#region serialiazable variables
	[SerializeField] Image backgroundSelected;
	[SerializeField] Image character;
	[SerializeField] Image health;
	[SerializeField] Image symbol;
	#endregion

	#region local variables
	CharacterSprite characterSprite;
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
		if(button == Button.Select)
        {
			if (IsHighlighted)
			{
				backgroundSelected.gameObject.SetActive(!backgroundSelected.gameObject.activeSelf);
				if (backgroundSelected.gameObject.activeSelf) { SceneReferences.NewCharacterScreen.SetChosenCharacter(Entity); }
				else { SceneReferences.NewCharacterScreen.SetChosenCharacter(null); }
			}
			else if (backgroundSelected.gameObject.activeSelf)
            {
				backgroundSelected.gameObject.SetActive(false);
            }
        }
    }
    #endregion

    #region public methods
    public override void Reset()
    {
        base.Reset();
		backgroundSelected.gameObject.SetActive(false);
    }

    public void ShowCharacter(Entity entity)
	{
		Entity = entity;

		characterSprite = SceneReferences.CharacterSprites[Entity.Sprite];
		character.gameObject.SetActive(true);
		character.sprite = characterSprite.ForwardSprite;
		health.gameObject.SetActive(true);
		health.sprite = SceneReferences.GetSpriteByHealth(Entity.CurrentHealth);

		symbol.gameObject.SetActive(true);
		symbol.sprite = SceneReferences.GetSrpiteBySymbol(Entity.Symbol);
	}
	#endregion

	#region coroutines
	#endregion
}
