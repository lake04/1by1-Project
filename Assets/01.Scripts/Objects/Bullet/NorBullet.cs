using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NorBullet : MonoBehaviour
{
    private WaitForSeconds wait2 = new WaitForSeconds(2);

    private void OnEnable()
    {
        StartCoroutine(DestroyWait());
    }

    private IEnumerator DestroyWait()
    {
        yield return wait2;
        GameManager.Instance.pool.BulletReturn(0, this.gameObject);
    }
}
