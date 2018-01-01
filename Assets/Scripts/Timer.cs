using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
		public float time;
		Text text;	

		void Awake ()
		{
			time = 0f;
			text = GetComponent <Text> ();
		}
		
		void Update ()
		{
			time += Time.deltaTime;	
			text.text = "Time: " + time.ToString("F3");
		}

		public float getScore()
		{
			return time;
		}

		public void resetTime()
		{
			time = 0f;
		}
}
