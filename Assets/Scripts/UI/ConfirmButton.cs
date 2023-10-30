using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : Slot
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
        base.OnButtonPressed(button);
        if (SceneReferences.Game.SelectedCharacter != null) { return; }
        if (button == Button.Select && confirmActive.gameObject.activeSelf && IsHighlighted)
        {
            SceneReferences.EventMessenger.SendStartRound();
            Reset();
        }
    }

    void OnPowerUpdated(int power)
    {
        if (power > 0) { confirmActive.gameObject.SetActive(true); }
        else { confirmActive.gameObject.SetActive(false); }
    }

    public override void RegisterEvents()
    {
        base.RegisterEvents();
        SceneReferences.EventMessenger.PowerUpdated += OnPowerUpdated;
    }

    public override void UnregisterEvents()
    {
        base.UnregisterEvents();
        try
        {
            SceneReferences.EventMessenger.PowerUpdated -= OnPowerUpdated;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
        }
    }
    #endregion

    #region public methods
    #endregion

    #region coroutines
    #endregion
}
