using UnityEngine;

public class SingletonDontDestroy<T> : MonoBehaviour where T : Component
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
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            print("Duplicate of the Singleton destroying " + _instance.GetType() + " " + gameObject.name);
            Destroy(gameObject);
        }
    }

    void OnApplicationQuit()
    {
        applicationIsQuitting = true;
    }
    #endregion
}
