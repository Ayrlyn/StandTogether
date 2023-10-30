using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{
	#region serialiazable variables
	[SerializeField] AudioSource audioSource;
	[SerializeField] AudioClip damage;
	[SerializeField] AudioClip directionalButton;
	[SerializeField] AudioClip failure;
	[SerializeField] AudioClip roundEnd;
	[SerializeField] AudioClip selectButton;
	[SerializeField] AudioClip success;
	#endregion

	#region local variables
	SceneReferences sceneReferences;
	#endregion

	#region getters and setters
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
    void OnButtonPressed(Button button)
    {
		if((int)button < 5)
        {
			if(audioSource.clip != directionalButton) { audioSource.clip = directionalButton; }
			audioSource.volume = 1;
			audioSource.Play();
        }
		else if(button == Button.Select)
		{
			if (audioSource.clip != selectButton) { audioSource.clip = selectButton; }
			audioSource.volume = 0.3f;
			audioSource.Play();
		}
    }

	void OnDamage(int value)
	{
		if (audioSource.clip != damage) { audioSource.clip = damage; }
		audioSource.volume = 0.3f;
		audioSource.Play();
	}

	void OnRoundEnd()
	{
		if (audioSource.clip != roundEnd) { audioSource.clip = roundEnd; }
		audioSource.volume = 0.8f;
		audioSource.Play();
	}

	void RegisterEvents()
    {
		SceneReferences.EventMessenger.ButtonPressed += OnButtonPressed;
		SceneReferences.EventMessenger.EndRound += OnRoundEnd;
		SceneReferences.EventMessenger.TakeDamage += OnDamage;
    }

	void UnregisterEvents()
    {
        try
		{
			SceneReferences.EventMessenger.ButtonPressed -= OnButtonPressed;
			SceneReferences.EventMessenger.EndRound -= OnRoundEnd;
			SceneReferences.EventMessenger.TakeDamage -= OnDamage;
		}
        catch (System.Exception e)
        {
			Debug.LogWarning(e);
        }
    }
	#endregion

	#region public methods
	public void PlayGameOver(bool victory)
    {
        if (victory)
		{
			if (audioSource.clip != success) { audioSource.clip = success; }
			audioSource.volume = 1;
			audioSource.Play();
		}
        else
		{
			if (audioSource.clip != failure) { audioSource.clip = failure; }
			audioSource.volume = 1;
			audioSource.Play();
		}
    }
	#endregion

	#region coroutines
	#endregion
}
