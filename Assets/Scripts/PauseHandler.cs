using UnityEngine;
using System.Collections;

public class PauseHandler : MonoBehaviour
{

		bool Paused = false;
	
		public GameObject canvas;
		public playerController1 playerScript;
	
		void Update ()
		{
				CheckInput ();
		
				if (Paused && !canvas.activeSelf) {
						ShowMenu ();
				} else if (!Paused && canvas.activeSelf) {
						HideMenu ();
				}
		}
	
		void ShowMenu ()
		{
				Paused = true;
				canvas.SetActive (true);
				playerScript.enabled = false;
				Time.timeScale = 0.0f;
		}
	
		void HideMenu ()
		{
				if (canvas.activeSelf) {
						Paused = false;
						canvas.SetActive (false);
						playerScript.enabled = true;
						Time.timeScale = 1.0f;
				}
		}
	
		void CheckInput ()
		{
				if (Input.GetButtonDown ("Paused")) {
						Paused = !Paused;
				}
		
		}

}
