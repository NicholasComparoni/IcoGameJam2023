using UnityEngine;

namespace ICO321 {
	public class EnemyDodgerAttack : EnemyAttack {
		public float bulletSpawnDistance;
		public float cooldown;
		public float range;

		public GameObject bullet;

		private bool resting;
		private float lastAttackTime;
		private Vector3 toPlayer;

		private new void Start() {
			resting = false;
			lastAttackTime = Mathf.NegativeInfinity;
		}

		private void Update() {
			if (!isPlayerAlive) return;
			toPlayer = (player.transform.position - transform.position);
			if (resting && Time.time - lastAttackTime > cooldown) {
				resting = false;
			}
			if (!resting && toPlayer.magnitude <= range) {
				Shoot();
				resting = true;
				lastAttackTime = Time.time;
			}
		}

		private void Shoot() {
			var newBullet = PoolManager.Instance.GetItem(bullet.name).GetComponent<Bullet>();
			Vector3 spawnPos = transform.position + toPlayer.normalized * bulletSpawnDistance;
			newBullet.transform.position = spawnPos;
			newBullet.transform.rotation = transform.rotation;
			newBullet.Direction = toPlayer.normalized;
		}
	}
}