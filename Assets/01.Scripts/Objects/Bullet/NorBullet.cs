using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public class NorBullet : MonoBehaviour
{
    private WaitForSeconds wait2 = new WaitForSeconds(2);
    private float damage;

    private void OnEnable()
    {
        StartCoroutine(DestroyWait());
    }

    private IEnumerator DestroyWait()
    {
        yield return wait2;
        GameManager.Instance.pool.BulletReturn(0, this.gameObject);
    }

    public void Init(float _damage)
    {
        damage = _damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            GameManager.Instance.pool.BulletReturn(0, this.gameObject);
        }
    }
}
