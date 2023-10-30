using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : Singleton<InputController>
{
    #region serialiazable variables
    #endregion

    #region local variables

    SceneReferences sceneReferences;
    #endregion

    #region getters and setters
    SceneReferences SceneReferences
    {
        get
        {
            if(sceneReferences == null) { sceneReferences = SceneReferences.Instance; }
            return sceneReferences;
        }
    }
    #endregion

    #region unity methods
    void Update()
    {
        if (SceneReferences.Game.RoundStarted) { return; }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) { SceneReferences.EventMessenger.SendButtonPressed(Button.Down); }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) { SceneReferences.EventMessenger.SendButtonPressed(Button.Left); }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) { SceneReferences.EventMessenger.SendButtonPressed(Button.Right); }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) { SceneReferences.EventMessenger.SendButtonPressed(Button.Up); }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) { SceneReferences.EventMessenger.SendButtonPressed(Button.Select); }
        if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
    }
    #endregion

    #region local methods
    #endregion

    #region public methods
    #endregion

    #region coroutines
    #endregion
}
