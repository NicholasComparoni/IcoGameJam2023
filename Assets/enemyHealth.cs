using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TypesUtility;

public class enemyHealth : MonoBehaviour
{
    public Phase phase;

    public bool Damage(Phase atkPhs)
    {
        if (atkPhs == phase)
        {
            Die();
            return true;
        }
        else return false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
