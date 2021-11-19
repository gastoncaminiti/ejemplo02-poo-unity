using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : Ship
{
    // Start is called before the first frame update
    [SerializeField] private GameObject originTwo;
    [SerializeField] private float rotationSpeed = 10f;

    // POLYMORPHISM
    public override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            InstantiateMunition(originOne.transform);
            InstantiateMunition(originTwo.transform);
        }
    }

    public override void Move()
    {
        float vAxis = Input.GetAxis("Vertical");
        float hAxis = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, hAxis * rotationSpeed * Time.deltaTime);
        transform.Translate(speedShip * new Vector3(0, 0, vAxis) * Time.deltaTime);
    }

    public override void DrawRaycast()
    {
        base.DrawRaycast();
        DrawRay(originTwo.transform);
    }
}
