using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    public float speed;
    public float damage;
    public float cooldown;

    public GameObject player;

    private bool resting;

    private float lastAttackTime;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        resting = false;
        lastAttackTime = Mathf.NegativeInfinity;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (resting && Time.time - lastAttackTime > cooldown)
        {
            resting = false;
        }
        if (!resting)
        {
            transform.LookAt(player.transform.position);
            rb.velocity = (player.transform.position - transform.position).normalized * speed;
        }
    }

    private void OnCollisionEnter2D(Collision collision)
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
