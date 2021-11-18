using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// INHERITANCE
public class BattleShip : Ship
{
    [SerializeField] private GameObject originTwo;
    [SerializeField] private GameObject target;
    [SerializeField] private float minDistance = 2f;
    // Start is called before the first frame update

    // POLYMORPHISM
    public override void Shoot()
    {
        base.Shoot();
        BroadcastRaycast(originTwo.transform);
    }

    public override void Move()
    {
        LookAtLerp(target);
        Vector3 direction = (target.transform.position - transform.position);
        if (direction.magnitude > minDistance)
        {
            transform.position += speedShip * direction.normalized * Time.deltaTime;
        }
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();
        DrawRay(originTwo.transform);
    }
}
