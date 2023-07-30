using ICO321;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDodgerAttack : MonoBehaviour {
	public float bulletSpawnDistance;
	public float cooldown;
	public float range;

	public GameObject player;
	public GameObject bullet;

	private bool resting;
	private float lastAttackTime;
	private Vector3 toPlayer;

	private void Start() {
		player = GameObject.Find("Player");
		resting = false;
		lastAttackTime = Mathf.NegativeInfinity;
		toPlayer = (player.transform.position - transform.position);

        player.GetComponent<PlayerHealth>().OnPlayerDeath += EnemyDodgerAttack_OnPlayerDeath;
	}

    private void EnemyDodgerAttack_OnPlayerDeath()
    {
        DestroyImmediate(this);
    }

    private void Update() {
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