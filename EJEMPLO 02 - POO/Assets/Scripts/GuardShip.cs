using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardShip : Ship
{
    [SerializeField] private GameObject originTwo;
    [SerializeField] private GameObject target;
    // Start is called before the first frame update

    // POLYMORPHISM
    public override void Shoot()
    {
        base.Shoot();
        BroadcastRaycast(originTwo.transform);
    }

    public override void Move()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speedShip * Time.deltaTime);
        transform.rotation = Quaternion.identity;
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();
        DrawRay(originTwo.transform);
    }
}
