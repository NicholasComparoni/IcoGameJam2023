using UnityEngine;

namespace ICO321 {
	public class EnemyActivator : MonoBehaviour {
		public void OnTriggerEnter2D(Collider2D other) {
			if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
				for (int i = 0; i < transform.GetChild(1).childCount; i++) {
					if (transform.GetChild(1).GetChild(i).GetComponent<EnemyAttack>()) {
						transform.GetChild(1).GetChild(i).GetComponent<EnemyAttack>().enabled = true;
					}
					// if (transform.GetChild(1).GetChild(i).GetComponent<EnemyMeleeAttack>()) {
					// 	transform.GetChild(1).GetChild(i).GetComponent<EnemyMeleeAttack>().enabled = true;
					// }
					// if (transform.GetChild(1).GetChild(i).GetComponent<EnemySniperAttack>()) {
					// 	transform.GetChild(1).GetChild(i).GetComponent<EnemySniperAttack>().enabled = true;
					// }
					// if (transform.GetChild(1).GetChild(i).GetComponent<EnemyDodgerAttack>()) {
					// 	transform.GetChild(1).GetChild(i).GetComponent<EnemyDodgerAttack>().enabled = true;
					// }
				}
				foreach (EnemyActivator trigger in transform.parent.GetComponentsInChildren<EnemyActivator>()) {
					trigger.OnTriggerEnter2D(other);
				}
			}
		}
	}
}