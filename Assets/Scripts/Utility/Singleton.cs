using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Component
{
    #region local variables
    static bool applicationIsQuitting = false;
    static T _instance;
    #endregion

    #region getters and setters
    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                return null;
            }
            return _instance;
        }
    }
    #endregion

    #region unity methods
    protected virtual void Awake()
    {
        //Checking Null & Checking Object Destroyed
        if (Instance == null || Instance.Equals(null))
        {
            _instance = this as T;
        }
        else
        {
            print("Duplicate of the Singleton destroying " + gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
    #endregion
}

