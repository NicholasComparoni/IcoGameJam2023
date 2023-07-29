using ICO321;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SniperAttack : MonoBehaviour
{
    public float bulletSpawnDistance;
    public float damage;
    public float cooldown;
    public float range;

    public GameObject player;

    private bool resting;

    private float lastAttackTime;

    private Vector3 toPlayer;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.Find("Player");
        resting = false;
        lastAttackTime = Mathf.NegativeInfinity;
        toPlayer = (player.transform.position - transform.position);
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        toPlayer = (player.transform.position - transform.position);

        float angle = Vector3.SignedAngle(transform.up, toPlayer, Vector3.forward);
        transform.RotateAround(transform.position, Vector3.forward, angle);

        if (resting && Time.time - lastAttackTime > cooldown)
        {
            resting = false;
        }
        if (!resting)
        {
            Shoot();
            resting = true;
            lastAttackTime = Time.time;
        }
    }

    private void Shoot()
    {
        var newBullet = PoolManager.Instance.GetItem("BlueBullet").GetComponent<Bullet>();
        newBullet.transform.position = transform.position + toPlayer.normalized * bulletSpawnDistance;
        newBullet.transform.rotation = Quaternion.identity;
        var direction = toPlayer.normalized;
        newBullet.Direction = direction;
    }
}
