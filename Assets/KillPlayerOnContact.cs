using ICO321;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ICO321
{
    public class KillPlayerOnContact : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                other.gameObject.GetComponent<PlayerHealth>().Die();
            }

        }
    }
}