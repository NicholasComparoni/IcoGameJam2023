using UnityEngine;

namespace ICO321 {
	public class EnemyMeleeAttack : MonoBehaviour {
		public float cooldown;
		public float damage;
		public float speed;

		public GameObject player;

		private bool resting;

		private float lastAttackTime;

		private Rigidbody2D rb;

		// Start is called before the first frame update
		private void Start() {
			player = GameObject.Find("Player");
			resting = false;
			lastAttackTime = Mathf.NegativeInfinity;
			rb = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		private void Update() {
			if (resting && Time.time - lastAttackTime > cooldown) {
				resting = false;
			}
			if (!resting) {
				float angle = Vector3.SignedAngle(transform.up, (player.transform.position - transform.position), Vector3.forward);
				transform.RotateAround(transform.position, Vector3.forward, angle);

				rb.velocity = (player.transform.position - transform.position).normalized * speed;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null) {
				//SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
				playerHealth.Damage();
				resting = true;
				lastAttackTime = Time.time;
				rb.velocity = Vector2.zero;
			}
		}
	}
}