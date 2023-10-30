using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartExitOptions : Slot
{
	public enum RestartOrExit { None, Restart, Exit }
	#region serialiazable variables
	[SerializeField] RestartOrExit restartOrExit;
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
        if(button == Button.Select && IsHighlighted)
        {
            switch (restartOrExit)
            {
                case RestartOrExit.Restart:
                    SceneReferences.HandUI.gameObject.SetActive(true);
                    SceneReferences.FieldUI.gameObject.SetActive(true);
                    SceneReferences.Game.NewGame();
                    SceneReferences.GameOverScreen.gameObject.SetActive(false);
                    break;
                case RestartOrExit.Exit:
                    Application.Quit();
                    break;
            }
        }
    }
    #endregion

    #region public methods
    #endregion

    #region coroutines
    #endregion
}
