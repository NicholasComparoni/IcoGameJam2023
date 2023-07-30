using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ICO321 {
	public class EnemyMeleeAttack : MonoBehaviour {
		public float cooldown;
		public float damage;
		public float speed;

		public LayerMask detectableLayers;

		public GameObject player;

		private bool resting;

		private float lastAttackTime;

		private Vector3 offset;
		private List<Vector3> waypoints;

		private Rigidbody2D rb;

		// Start is called before the first frame update
		private void Start() {
			player = GameObject.Find("Player");
			resting = false;
			offset = Vector3.zero;
			lastAttackTime = Mathf.NegativeInfinity;
			waypoints = new List<Vector3>();
			rb = GetComponent<Rigidbody2D>();
		}

		// Update is called once per frame
		private void Update() {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 30, detectableLayers);
			if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				waypoints.Clear();
				waypoints.Add(player.transform.position - transform.parent.position);
			}
			else if (waypoints.Count > 0)
			{
				RaycastHit2D waypointHit;
				foreach (Vector3 waypoint in waypoints)
				{
					waypointHit = Physics2D.Raycast(transform.parent.position + waypoint, player.transform.position - (transform.parent.position + waypoint), 30, detectableLayers);
					if (waypointHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
					{
						foreach (Vector3 furtherWaypoint in waypoints)
						{
							if (waypoints.IndexOf(furtherWaypoint) <= waypoints.IndexOf(waypoint)) continue;
							else waypoints.Remove(furtherWaypoint);
						}
						waypoints.Add((Vector3)(waypointHit.point) - transform.parent.position);
						break;
					}
					else if (waypoints.IndexOf(waypoint) == (waypoints.Count - 1))
					{
                        waypoints.Add((Vector3)(waypointHit.point + (waypointHit.normal * GetComponent<CircleCollider2D>().radius)) - transform.parent.position);
                        waypoints.Add((Vector3)(waypointHit.point) - transform.parent.position);
                    }
				}
			}
			else
			{
				waypoints.Add((Vector3)(hit.point + (hit.normal * GetComponent<CircleCollider2D>().radius)) - transform.parent.position);
				waypoints.Add((Vector3)(hit.point) - transform.parent.position);
			}

            Vector3 debug = transform.position;

            foreach (Vector3 waypoint in waypoints)
            {
                Debug.DrawLine(debug, transform.parent.position + waypoints.ToArray()[waypoints.IndexOf(waypoint)], Color.green, 0.1f);
                debug = transform.parent.position + waypoint;
            }

            if (resting && Time.time - lastAttackTime > cooldown) {
				resting = false;
			}
			if (!resting) {
				foreach (Vector3 waypoint in waypoints)
                {
                    if (((transform.parent.position + waypoint) - transform.position).magnitude < 0.1) waypoints.Remove(waypoint);
				}

				Vector3 destination = transform.parent.position + waypoints.ToArray()[0];

                float angle = Vector3.SignedAngle(transform.up, (destination - transform.position), Vector3.forward);
				transform.RotateAround(transform.position, Vector3.forward, angle);

				rb.velocity = (destination - transform.position).normalized * speed;
			}
		}

		private void OnCollisionEnter2D(Collision2D collision) {
			var playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
			if (playerHealth != null) {
				playerHealth.Damage();
				resting = true;
				lastAttackTime = Time.time;
				rb.velocity = Vector2.zero;
			}
		}
	}
}