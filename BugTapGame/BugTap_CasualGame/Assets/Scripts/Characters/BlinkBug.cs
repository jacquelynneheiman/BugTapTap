using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkBug : Enemy
{
    public float blinkInterval;
    private float timer;

    public float[] laneXPos;

    private void Start()
    {
        timer = blinkInterval;
    }

    protected override void OnMouseDown()
    {
        health--;

        if (hasHelmut)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            speed += 1f;
        }

        if (health == 0)
        {
            level.correct++;
            level.score += points;

            GameObject particle = Instantiate(clickAnim, this.transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(particle, .5f);
            Destroy(this.gameObject, .5f);
        }

        GameObject anim = Instantiate(clickAnim, transform.position, Quaternion.identity);
        Destroy(anim, .5f);
    }

    protected override void Update()
    {
        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            int randomLane = Random.Range(0, laneXPos.Length);
            transform.position = new Vector2(laneXPos[randomLane], transform.position.y);

            timer = blinkInterval;
        }

        base.Update();
    }

}
