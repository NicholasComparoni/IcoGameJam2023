using ICO321;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICO321
{
    public class MeleeAttack : MonoBehaviour
    {
        public float cooldown;
        public float damage;
        public float speed;

        public GameObject player;

        private bool resting;

        private float lastAttackTime;

        private Rigidbody2D rb;

        // Start is called before the first frame update
        private void Start()
        {

            player = GameObject.Find("Player");
            resting = false;
            lastAttackTime = Mathf.NegativeInfinity;
            rb = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (resting && Time.time - lastAttackTime > cooldown)
            {
                resting = false;
            }
            if (!resting)
            {
                float angle = Vector3.SignedAngle(transform.up, (player.transform.position - transform.position), Vector3.forward);
                transform.RotateAround(transform.position, Vector3.forward, angle);

                rb.velocity = (player.transform.position - transform.position).normalized * speed;
            }
        }


        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!resting && collision.collider.gameObject == player)
            {
                SendMessage("Damage", damage, SendMessageOptions.DontRequireReceiver);
                resting = true;
                lastAttackTime = Time.time;
                rb.velocity = Vector2.zero;
            }
        }
    }
}