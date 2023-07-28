using System;
using UnityEngine;

namespace ICO321 {
	public class PlayerFire : MonoBehaviour {
		public GameObject bullet;
		public Transform turret;
		public Transform muzzle;
		[Space] [SerializeField] private float coolDown;
		private float coolDownCounter;

		private void Awake() {
			coolDownCounter = -100;
		}

		private void Update() {
			if (coolDownCounter < 0 && Input.GetButtonDown("Fire1")) {
				var newBullet = PoolManager.Instance.GetItem(bullet.name).GetComponent<Bullet>();
				newBullet.transform.position = muzzle.position;
				newBullet.transform.rotation = turret.rotation;
				var direction = muzzle.transform.position - turret.transform.position;
				newBullet.Direction = direction;
				newBullet.Phase = PhaseManager.Instance.CurrentPhase;
				coolDownCounter = coolDown;
			}
			coolDownCounter -= Time.deltaTime;
		}
	}
}