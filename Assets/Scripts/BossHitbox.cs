using UnityEngine;
using System.Collections;

public class BossHitbox : MonoBehaviour
{

		public int health = 5;
		bool shieldIsDown = false;
		public GameObject redShield;
		public GameObject blueShield;
		public BigBossScript boss;

	
		// Update is called once per frame
		void Update ()
		{
				if (shieldIsDown) {
						this.GetComponent<Collider2D>().enabled = true;
				} else {
						this.GetComponent<Collider2D>().enabled = false;
				}
		}
	
		public void ShieldDown (bool isDown)
		{
				shieldIsDown = isDown;
		}
	
		void OnCollisionEnter2D (Collision2D col)
		{
				if (col.gameObject.tag == "Explosion") {
						health -= 1;
						ActivateShielding ();
				} else if (col.gameObject.tag == "Bomb") {
						col.gameObject.GetComponent<BombExploder> ().startFuse ();
				}
		}
	
		void ActivateShielding ()
		{
				boss.blueOrb.destroyed = false;
				boss.redOrb.destroyed = false;
				boss.Enrage ();
				redShield.SetActive (true);
				blueShield.SetActive (true);
				shieldIsDown = false;
		}
}
