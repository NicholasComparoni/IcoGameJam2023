using UnityEngine;

namespace ICO321 {
	public class EnemySniperAttack : EnemyAttack {
		public float bulletSpawnDistance;
		public float cooldown;
		public float range;
		public float speed;

		public GameObject bullet;

		private bool resting;
		private float lastAttackTime;
		private Vector3 toPlayer;
		private Rigidbody2D rb;

		private void Start() {
			resting = false;
			lastAttackTime = Mathf.NegativeInfinity;
			toPlayer = (player.transform.position - transform.position);
			rb = GetComponent<Rigidbody2D>();
		}

		private void Update() {
			toPlayer = (player.transform.position - transform.position);

			float angle = Vector3.SignedAngle(transform.up, toPlayer, Vector3.forward);
			transform.RotateAround(transform.position, Vector3.forward, angle);

			if (resting && Time.time - lastAttackTime > cooldown) {
				resting = false;
			}
			if (!resting) {
				if (toPlayer.magnitude > range) {
					rb.velocity = (player.transform.position - transform.position).normalized * speed;
				}
				else {
					rb.velocity = Vector2.zero;
					Shoot();
					resting = true;
					lastAttackTime = Time.time;
				}
			}
		}

		private void Shoot() {
			var newBullet = PoolManager.Instance.GetItem(bullet.name).GetComponent<Bullet>();
			Vector3 spawnPos = transform.position + toPlayer.normalized * bulletSpawnDistance;
			Vector3 dir = LeadShot(player.transform.position, (Vector3)player.GetComponent<Rigidbody2D>().velocity, spawnPos, newBullet.speed);
			newBullet.transform.position = spawnPos;
			newBullet.transform.rotation = transform.rotation;
			newBullet.Direction = dir.normalized;
		}

		private Vector3 LeadShot(Vector3 targetPosition, Vector3 targetVelocity, Vector3 startPosition, float projectileSpeed) {
			float curDistance = (targetPosition - startPosition).magnitude;
			float distanceCoefficient = (((targetPosition + (targetVelocity * curDistance / projectileSpeed))) - startPosition).magnitude / projectileSpeed;
			return (((targetPosition + (targetVelocity * distanceCoefficient)) - startPosition));
		}
	}
}