using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSlot : Slot
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
        base.OnButtonPressed(button);
		if (button == Button.Select)
		{
			if (IsHighlighted && SceneReferences.Game.SelectedCharacter == null && Entity != null && Entity.CurrentHealth > 0)
			{
				backgroundSelected.gameObject.SetActive(true);
				SceneReferences.EventMessenger.SendCharacterSelected(Entity);
				return;
			}
			if (IsHighlighted && Entity != null && SceneReferences.Game.SelectedCharacter == Entity)
			{
				backgroundSelected.gameObject.SetActive(false);
				SceneReferences.EventMessenger.SendCharacterDropped(Entity);
				return;
			}
			if (IsHighlighted && Entity == null && SceneReferences.Game.SelectedCharacter != null)
			{
				ShowCharacter(SceneReferences.Game.SelectedCharacter);
				SceneReferences.EventMessenger.SendCharacterDropped(Entity);
				return;
			}
		}
	}

	void OnCharacterDropped(Entity entity)
	{
		if (!IsHighlighted && Entity == entity)
		{
			ClearEntity();
		}
	}

	void OnRoundEnd()
    {
		if(SlotType != SlotType.Field) { return; }

		character.gameObject.SetActive(false);
		health.gameObject.SetActive(false);
		symbol.gameObject.SetActive(false);
		backgroundSelected.gameObject.SetActive(false);
		Entity = null;
	}

	void OnTakeDamage(int damage)
	{
		if (SlotType != SlotType.Field || Entity == null) { return; }

		SetHealth(Entity.CurrentHealth);
		StartCoroutine(SetAttack());
		if (Entity.CurrentHealth <= 0) { StartCoroutine(SetHurt()); }
	}

    public override void RegisterEvents()
    {
        base.RegisterEvents();
		SceneReferences.EventMessenger.CharacterDropped += OnCharacterDropped;
		SceneReferences.EventMessenger.EndRound += OnRoundEnd;
		SceneReferences.EventMessenger.TakeDamage += OnTakeDamage;
	}

    public override void UnregisterEvents()
    {
        base.UnregisterEvents();
		try
		{
			SceneReferences.EventMessenger.CharacterDropped -= OnCharacterDropped;
			SceneReferences.EventMessenger.EndRound -= OnRoundEnd;
			SceneReferences.EventMessenger.TakeDamage -= OnTakeDamage;
		}
		catch (Exception e)
		{
			Debug.LogWarning(e);
		}
	}
	#endregion

	#region public methods
	public void ClearEntity()
	{
		character.gameObject.SetActive(false);
		health.gameObject.SetActive(false);
		symbol.gameObject.SetActive(false);
		backgroundSelected.gameObject.SetActive(false);
		Entity = null;
	}

	public void SetHealth(int value)
	{
		health.sprite = SceneReferences.GetSpriteByHealth(value);
	}

	public void ShowCharacter(Entity entity)
	{
		if(!this.gameObject.activeInHierarchy || Entity != null) { return; }
		Entity = entity;

		characterSprite = SceneReferences.CharacterSprites[Entity.Sprite];
		character.gameObject.SetActive(true);
		switch (SlotType)
		{
			case SlotType.Hand:
				character.sprite = characterSprite.ForwardSprite;
				break;
			case SlotType.Field:
				character.sprite = characterSprite.ReadySprite;
				break;
			case SlotType.Monster:
				break;
			default:
				break;
		}
		health.gameObject.SetActive(true);
		SetHealth(Entity.CurrentHealth);

		symbol.gameObject.SetActive(true);
		symbol.sprite = SceneReferences.GetSrpiteBySymbol(Entity.Symbol);
	}
	#endregion

	#region coroutines
	IEnumerator SetAttack()
    {
		character.sprite = characterSprite.AttackSprite;
		yield return new WaitForSecondsRealtime(0.25f);
		character.sprite = characterSprite.ReadySprite;
	}

	IEnumerator SetHurt()
	{
		character.sprite = characterSprite.HurtSprite;
		yield return new WaitForSecondsRealtime(0.25f);
		character.gameObject.SetActive(false);
		health.gameObject.SetActive(false);
		symbol.gameObject.SetActive(false);
		SceneReferences.EventMessenger.SendEntityDefeated(Entity);
		Entity = null;
		StartCoroutine(SceneReferences.Game.DelayedPowerUpdate());
	}
	#endregion
}
