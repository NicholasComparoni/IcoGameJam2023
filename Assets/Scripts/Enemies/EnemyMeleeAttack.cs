using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace ICO321 {
	public class EnemyMeleeAttack : EnemyAttack {
		public enum MeleeStatus {
			Idle = 0,
			Alert = 1,
			Resting = 2,
		}

		[SerializeField] public float cooldown;
		[SerializeField] public float speed;
		[SerializeField] private float turningSpeed;
		[SerializeField] private float viewDistance = 10;
		[SerializeField] public LayerMask detectableLayers;
		private MeleeStatus status = MeleeStatus.Idle;

		private float targetAngle;
		private CircleCollider2D collider;
		private float lastAttackTime;
		private List<Vector3> waypoints;
		private Rigidbody2D rb;

		private new void Awake() {
			base.Awake();
			collider = GetComponent<CircleCollider2D>();
		}

		private new void Start() {
			base.Start();
			lastAttackTime = Mathf.NegativeInfinity;
			waypoints = new List<Vector3>();
			rb = GetComponent<Rigidbody2D>();
			rb.constraints = RigidbodyConstraints2D.FreezeRotation;
		}

		private RaycastHit2D SeekPlayer(out bool actualPlayer) {
			var position = transform.position;
			RaycastHit2D hit = Physics2D.Raycast(position, player.transform.position - position, viewDistance, detectableLayers);
			if (hit && hit.collider.gameObject.layer == playerLayer) {
				waypoints.Clear();
				waypoints.Add(player.transform.position - transform.parent.position);
				status = MeleeStatus.Alert;
				actualPlayer = true;
			}
			actualPlayer = false;
			return hit;
		}

		private void Update() {
			bool isplayer;
			switch (status) {
				case MeleeStatus.Idle:
					var canSeePlayer = SeekPlayer(out isplayer);
					if (isplayer) status = MeleeStatus.Alert;
					break;
				case MeleeStatus.Alert:
					if (waypoints.Count > 0) {
						bool playerFoundEarly = false;
						bool playerFoundLast = false;
						int removeIndex = 0;
						int removeCount = 0;
						Vector3 newPoint1 = Vector3.zero;
						Vector3 newPoint2 = Vector3.zero;
						RaycastHit2D waypointHit;
						foreach (Vector3 waypoint in waypoints) {
							waypointHit = Physics2D.Raycast(transform.parent.position + waypoint, player.transform.position - (transform.parent.position + waypoint), 30, detectableLayers);
							if (waypointHit && waypointHit.collider.gameObject.layer == LayerMask.NameToLayer("Player")) {
								playerFoundEarly = true;
								removeIndex = waypoints.IndexOf(waypoint);
								removeCount = waypoints.Count - waypoints.IndexOf(waypoint);
								newPoint1 = (Vector3)(waypointHit.point) - transform.parent.position;
								break;
							}
							else if (waypoints.IndexOf(waypoint) == (waypoints.Count - 1)) {
								playerFoundLast = true;
								newPoint1 = (Vector3)(waypointHit.point + (waypointHit.normal * collider.radius)) - transform.parent.position;
								newPoint2 = (Vector3)(waypointHit.point) - transform.parent.position;
							}
						}
						if (playerFoundEarly) {
							waypoints.RemoveRange(removeIndex, removeCount);
							waypoints.Add(newPoint1);
						}
						else if (playerFoundLast) {
							waypoints.Add(newPoint1);
							waypoints.Add(newPoint2);
						}
					}
					else {
						waypoints.Add((player.transform.position) - transform.parent.position);
					}

					Vector3 ToDelete = Vector3.zero;
					foreach (Vector3 waypoint in waypoints) {
						if (((transform.parent.position + waypoint) - transform.position).magnitude < collider.radius / 2f) ToDelete = waypoint;
						break;
					}
					if (waypoints.Contains(ToDelete)) {
						waypoints.Remove(ToDelete);
					}

					Vector3 destination = transform.parent.position + waypoints.ToArray()[0];

					float angle = Vector3.SignedAngle(transform.up, (destination - transform.position), Vector3.forward);
					transform.RotateAround(transform.position, Vector3.forward, angle);

					rb.velocity = (destination - transform.position).normalized * speed;
					break;
				case MeleeStatus.Resting:
					if (Time.time - lastAttackTime > cooldown) {
						rb.drag = 0.0f;
						rb.angularDrag = 0.0f;
						//resting = false;
						status = MeleeStatus.Idle;
					}
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null) {
				playerHealth.Damage();
				lastAttackTime = Time.time;
				rb.velocity = Vector2.zero;
				rb.drag = 20.0f;
				rb.angularDrag = 10.0f;
				status = MeleeStatus.Resting;
			}
		}
	}
}