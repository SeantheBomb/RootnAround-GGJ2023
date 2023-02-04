using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class NestContainer : MonoBehaviour
{

    public List<RootItem> items;
    Collider2D c;

    private void Start()
    {
        c = GetComponent<Collider2D>();
    }

    public void AddItem(RootItem item)
    {
        items.Add(item);
        if(item.TryGetComponent(out SpriteRenderer sr))
        {
            sr.sortingOrder = -1;
        }
        item.transform.parent = transform;
        item.transform.position = GetPlacePoint();
    }

    Vector3 GetPlacePoint()
    {
        Vector2 random = Random.insideUnitCircle;
        random.x *= c.bounds.extents.x;
        random.y *= c.bounds.extents.y;
        return c.bounds.center + (Vector3)random;
    }

}
