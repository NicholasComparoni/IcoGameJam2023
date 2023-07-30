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

            player.GetComponent<PlayerHealth>().OnPlayerDeath += EnemyMeleeAttack_OnPlayerDeath;
		}

        private void EnemyMeleeAttack_OnPlayerDeath()
        {
            DestroyImmediate(this);
        }

        // Update is called once per frame
        private void Update() {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, player.transform.position - transform.position, 30, detectableLayers);
			if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
			{
				waypoints.Clear();
				waypoints.Add(player.transform.position - transform.parent.position);
			}
			else if (waypoints.Count > 0)
			{
				bool playerFoundEarly = false;
				bool playerFoundLast = false;
				int removeIndex = 0;
				int removeCount = 0;
				Vector3 newPoint1 = Vector3.zero;
                Vector3 newPoint2 = Vector3.zero;
                RaycastHit2D waypointHit;
				foreach (Vector3 waypoint in waypoints)
				{
					waypointHit = Physics2D.Raycast(transform.parent.position + waypoint, player.transform.position - (transform.parent.position + waypoint), 30, detectableLayers);
					if (waypointHit && waypointHit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
					{
						playerFoundEarly = true;
						removeIndex = waypoints.IndexOf(waypoint);
						removeCount = waypoints.Count - waypoints.IndexOf(waypoint);
						newPoint1 = (Vector3)(waypointHit.point) - transform.parent.position;
						break;
					}
					else if (waypoints.IndexOf(waypoint) == (waypoints.Count - 1))
					{
						playerFoundLast = true;
						newPoint1 = (Vector3)(waypointHit.point + (waypointHit.normal * GetComponent<CircleCollider2D>().radius)) - transform.parent.position;
						newPoint2 = (Vector3)(waypointHit.point) - transform.parent.position;
                    }
				}
				if (playerFoundEarly)
                {
                    waypoints.RemoveRange(removeIndex, removeCount);
                    waypoints.Add(newPoint1);
                }
				else if (playerFoundLast)
				{
					waypoints.Add(newPoint1);
					waypoints.Add(newPoint2);
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
				Vector3 delenda = Vector3.zero;
				foreach (Vector3 waypoint in waypoints)
                {
                    if (((transform.parent.position + waypoint) - transform.position).magnitude < 0.1) delenda = waypoint;
					break;
				}
				if (waypoints.Contains(delenda)) { waypoints.Remove(delenda); }

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