using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bug bug = collision.GetComponent<Bug>();

        if(collision.CompareTag("Enemy"))
        {
            if(bug)
            {
                bug.level.LoseHealth();
                bug.level.incorrect++;
            }
        }
        else
        {
            if (bug)
            {
                bug.level.score += bug.points;
                bug.level.correct++; 
            }
        }

        Destroy(collision.gameObject);
    }
}
