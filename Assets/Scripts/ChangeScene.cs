using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
		int highestLevel;
		public Transform levelSelect;
		public GameObject panel;
		 
		void Start ()
		{
				highestLevel = PlayerPrefs.GetInt ("HighestLevel", 1);
				if (highestLevel >= 5) {
						levelSelect.GetComponent<Button> ().interactable = true;
				} else {
						levelSelect.GetComponent<Button> ().interactable = false;
				}
				
		}
	
		public void ChangeSceneTo (int sceneNumber)
		{
				Application.LoadLevel (sceneNumber);
				Time.timeScale = 1.0f;
		}
		
		public void ToggleLevelsView (GameObject levelButtons)
		{
				if (levelButtons.activeSelf) {
						levelButtons.SetActive (false);
				} else {
						levelButtons.SetActive (true);
				}
		}
		
		public void RestartLevel ()
		{
				int currentLevel = Application.loadedLevel;
				ChangeSceneTo (currentLevel);
		}
		
		public void ResumeLevel ()
		{
				ChangeSceneTo (PlayerPrefs.GetInt ("CurrentLevel", 1));
		}
		
		public void ShowScores ()
		{
				if (panel.activeSelf) {
						panel.SetActive (false);
				} else {
						panel.SetActive (true);
				}
		}
}
