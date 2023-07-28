using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour {
	public float speed;
	public float damage;
	public float cooldown;

	public GameObject player;

	private bool resting;

	private float lastAttackTime;

	private Rigidbody2D rb;

	// Start is called before the first frame update
	private void Start() {
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
			transform.LookAt(player.transform.position);
			rb.velocity = (player.transform.position - transform.position).normalized * speed;
		}
	}


	private void OnCollisionEnter2D(Collision2D collision) {
		if (!resting && collision.collider.gameObject == player) {
			SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
			resting = true;
			lastAttackTime = Time.time;
			rb.velocity = Vector2.zero;
		}
	}
}