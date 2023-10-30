using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] SlotType slotType;

	[SerializeField] Image backgroundNormal;
	[SerializeField] Image backgroundHighlighted;
	[SerializeField] Slot slotDown;
	[SerializeField] Slot slotLeft;
	[SerializeField] Slot slotRight;
	[SerializeField] Slot slotUp;
	#endregion

	#region local variables
	Entity entity;
	SceneReferences sceneReferences;
	#endregion

	#region getters and setters
	public Entity Entity { get { return entity; } set { entity = value; } }
	public bool IsHighlighted { get { return backgroundHighlighted.gameObject.activeInHierarchy; } }
	public SlotType SlotType { get { return slotType; } }
	public SceneReferences SceneReferences
	{
		get
		{
			if (sceneReferences == null) { sceneReferences = SceneReferences.Instance; }
			return sceneReferences;
		}
	}
    #endregion

    #region unity methods
    void Start()
    {
		RegisterEvents();
    }

    void OnDestroy()
    {
		UnregisterEvents();
    }
    #endregion

    #region local methods
	public virtual void OnButtonPressed(Button button)
    {
        if (!this.gameObject.activeInHierarchy) { return; }

		if((int)button < 5)
		{
			if (!IsHighlighted) { return; }
			backgroundNormal.gameObject.SetActive(true);
			backgroundHighlighted.gameObject.SetActive(false);
			StartCoroutine(DelayedOnDirectionPressed(button));
			return;
		}
    }

	public virtual void RegisterEvents()
	{
		SceneReferences.EventMessenger.ButtonPressed += OnButtonPressed;
	}

	public virtual void UnregisterEvents()
	{
		try
		{
			SceneReferences.EventMessenger.ButtonPressed -= OnButtonPressed;
		}
		catch (Exception e)
		{
			Debug.LogWarning(e);
		}
	}
    #endregion

    #region public methods
    public void MarkHighlightActive()
    {
        if (IsHighlighted) { return; }

		backgroundNormal.gameObject.SetActive(false);
		backgroundHighlighted.gameObject.SetActive(true);
    }

    public virtual void Reset()
	{
		backgroundNormal.gameObject.SetActive(true);
		backgroundHighlighted.gameObject.SetActive(false);
		Entity = null;
	}
    #endregion

    #region coroutines
    IEnumerator DelayedOnDirectionPressed(Button direction)
    {
		yield return new WaitForSecondsRealtime(0.1f);
		switch (direction)
		{
			case Button.Down:
				slotDown.MarkHighlightActive();
				break;
			case Button.Left:
				slotLeft.MarkHighlightActive();
				break;
			case Button.Right:
				slotRight.MarkHighlightActive();
				break;
			case Button.Up:
				slotUp.MarkHighlightActive();
				break;
			default:
				Debug.LogError($"Invalid direction: {direction}");
				break;
		}
	}
	#endregion
}
