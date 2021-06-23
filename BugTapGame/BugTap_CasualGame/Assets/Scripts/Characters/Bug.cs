using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public Level level;

    public float speed;

    public int points;

    public int health;

    public GameObject clickAnim;

    protected virtual void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    protected virtual void OnMouseDown()
    {
        GameObject particle = Instantiate(clickAnim, this.transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(particle, .5f);
        Destroy(this.gameObject, .5f);
    }
}
