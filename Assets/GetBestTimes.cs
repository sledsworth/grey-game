using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetBestTimes : MonoBehaviour
{

		public Text text;
	
		// Use this for initialization
		void Start ()
		{
				for (int i = 1; i < 6; i++) {
						text.text += "\n";
						text.text += " Level " + i + ": ";
						if (PlayerPrefs.GetFloat ("BestTime" + i) != 0) {
								text.text += PlayerPrefs.GetFloat ("BestTime" + i);
						} else {
								text.text += "-------";
						}
						
				}
		}
	
}
