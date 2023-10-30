using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
	#region serialiazable variables
	[SerializeField] RestartExitOptions restart;
	[SerializeField] RestartExitOptions exit;
	[SerializeField] GameObject win;
	[SerializeField] GameObject loss;
	#endregion

	#region local variables
	#endregion

	#region getters and setters
	#endregion

	#region unity methods
	#endregion

	#region local methods
	#endregion

	#region public methods
	public void ShowGameOverScreen(bool victory)
    {
		this.gameObject.SetActive(true);

		win.SetActive(victory);
		loss.SetActive(!victory);

		restart.MarkHighlightActive();
		exit.Reset();
    }
	#endregion

	#region coroutines
	#endregion
}
