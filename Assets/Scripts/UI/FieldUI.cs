using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldUI : Singleton<FieldUI>
{
	#region serialiazable variables
    [SerializeField] Image powerTens;
    [SerializeField] Image powerUnits;
    #endregion

    #region local variables
    SceneReferences sceneReferences;
    #endregion

    #region getters and setters
    SceneReferences SceneReferences
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
    void DisplayPower(int power)
    {
        powerUnits.sprite = SceneReferences.GetSpriteByHealth(power % 10);
        powerTens.sprite = SceneReferences.GetSpriteByHealth(power / 10);
    }

    void OnPowerUpdated(int power)
    {
        DisplayPower(power);
    }

    void RegisterEvents()
    {
        SceneReferences.EventMessenger.PowerUpdated += OnPowerUpdated;
    }

    void UnregisterEvents()
    {
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
