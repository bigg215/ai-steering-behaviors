using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public int health = 100;
    private new SpriteRenderer renderer;
    private Color initialColor;

    public void TakeDamage(int damage)
    {
        health -= damage;

        renderer = GetComponent<SpriteRenderer>();
        initialColor = renderer.color;

        StartCoroutine(Flash());

        if (health <=0)
        {
            Die();
        }

    }

    void Die()
    {
        Destroy(this.gameObject);
    }

    IEnumerator Flash()
    {
        for (int n = 0; n < 2; n++)
        {
            renderer.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
            renderer.color = initialColor;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
