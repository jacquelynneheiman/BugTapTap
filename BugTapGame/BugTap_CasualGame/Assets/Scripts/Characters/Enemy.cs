using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Bug
{
    public bool hasHelmut;

    protected override void OnMouseDown()
    {
        health--;

        if(hasHelmut)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            speed += 1f;
        }

        if (health == 0)
        {
            level.correct++;
            level.score += points;
            base.OnMouseDown();
        }

        GameObject anim = Instantiate(clickAnim, transform.position, Quaternion.identity);
        Destroy(anim, .5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Bug bug = collision.gameObject.GetComponent<Bug>();

        if(bug && bug.CompareTag("Friendly"))
        {
            bug.level.incorrect++;
            bug.level.LoseHealth();
            Destroy(collision.gameObject);
        }
    }
}
