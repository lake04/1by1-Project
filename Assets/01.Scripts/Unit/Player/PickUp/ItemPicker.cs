using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    public Vector2 pickupSize = new Vector2(1.5f, 1.5f);

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            PickUp();
        }
    }

    void PickUp()
    {
        Debug.Log("아이템 먹기");
        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, pickupSize, 0f);

        foreach (var hit in hits)
        {
            Debug.Log(hit.GetComponent<Gun>());
            IPickupable pickup = hit.GetComponent<IPickupable>();
            if (pickup != null)
            {
                pickup.OnPickup();
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, pickupSize);
    }
}
