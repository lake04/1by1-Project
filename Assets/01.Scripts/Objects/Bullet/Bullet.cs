using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private WaitForSeconds wait = new WaitForSeconds(2);
    public float damage;
    public float speed;
    public Element bulletElement;
    private SpriteRenderer spriteRenderer;

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(DestroyWait());
    }

    private IEnumerator DestroyWait()
    {
        yield return wait;
        GameManager.Instance.pool.BulletReturn(0, this.gameObject);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            GameManager.Instance.pool.BulletReturn(0, this.gameObject);
        }
    }

    private void ElementEffect(Enemy enemy)
    {
        
    }


    public void SetSprite(Sprite sprite)
    {
        if (sprite != null)
        {
            spriteRenderer.sprite = sprite;
        }
    }

}
