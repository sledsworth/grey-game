using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BestTime : MonoBehaviour
{

		private float highScore;
		public Timer timer;
		public Text text;

		void Start ()
		{
				highScore = PlayerPrefs.GetFloat ("BestTime" + Application.loadedLevel);
				if (highScore != 0) {
						text.text = "Best Time: " + highScore.ToString ("F3");
				} else {
						text.text = "Best Time: -------";
				}
		}

		void OnCollisionEnter2D (Collision2D col)
		{
				float score = timer.getScore ();
				if (col.gameObject.tag == "Player") {
						if (score < highScore || highScore == 0) {
								highScore = score;
								PlayerPrefs.SetFloat ("BestTime" + Application.loadedLevel, highScore);
								text.text = "Best Time: " + highScore.ToString ("F3");
						}
						timer.resetTime ();
						int i = Application.loadedLevel;
						PlayerPrefs.SetInt ("CurrentLevel", i + 1);
						int highestLevel = PlayerPrefs.GetInt ("HighestLevel", 1);
						if (Application.loadedLevel + 1 >= highestLevel) {
								PlayerPrefs.SetInt ("HighestLevel", Application.loadedLevel + 1);
						}
						Application.LoadLevel (i + 1);
				}
		}
	
}
