using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public enum shipTypes { Battle,Cargo, Guard, Player };
        
    [SerializeField] private int hpPoints;
    [SerializeField] private int shieldPoints;
    [SerializeField] private int damagePoints;
    [SerializeField] private int shootCooldown = 2;

    [SerializeField] protected float speedShip;
    [SerializeField] private   float distanceRay = 10f;
    [SerializeField] private   float timerShoot = 0;
    [SerializeField] private   float forcePower = 5;

    [SerializeField] private shipTypes shipType;
    [SerializeField] private shipTypes targetType;

    [SerializeField] private GameObject munitionPrefab;
    [SerializeField] private GameObject originOne;

    public bool canShoot = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canShoot)
        {
           Shoot();
        }
        else
        {
           timerShoot += Time.deltaTime;
        }

        if (timerShoot > shootCooldown)
        {
            canShoot = true;
        }

        Move();
    }
    // ENCAPSULATION
    public virtual void Move()
    {
        transform.Translate(speedShip * Time.deltaTime * Vector3.forward);
    }

    public virtual void Shoot()
    {
        BroadcastRaycast(originOne.transform);
    }

    private void DealDamage(int damage)
    { 
        if(shieldPoints > 0)
        {

            shieldPoints -= damage;
            
        }
        else
        {
            hpPoints -= damage;
            if(hpPoints < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public shipTypes GetTargetType()
    {
        return shipType;
    }

   protected void BroadcastRaycast(Transform origen)
   {
        RaycastHit hit;
        int layerMask = 1 << 31;
        layerMask = ~layerMask;

        if (Physics.Raycast(origen.position, origen.TransformDirection(Vector3.forward), out hit, distanceRay,layerMask))
        {
            Ship t = hit.transform.gameObject.GetComponent<Ship>();
            if ((t != null) && (t.GetTargetType() == targetType))
            {
                canShoot = false;
                timerShoot = 0;
                GameObject b = Instantiate(munitionPrefab, origen.position, munitionPrefab.transform.rotation);
                b.GetComponent<Rigidbody>().AddForce(origen.TransformDirection(Vector3.forward) * forcePower, ForceMode.Impulse);
            }
        }
    }

  protected void DrawRay(Transform origen)
    {
        Gizmos.color = Color.blue;
        Vector3 direction = origen.TransformDirection(Vector3.forward) * distanceRay;
        Gizmos.DrawRay(origen.position, direction);
    }

   public virtual void DrawRaycast()
   {
        DrawRay(originOne.transform);
   }

    //METODO PARA MIRAR OBJETO CON LERP
    protected void LookAtLerp(GameObject lookObject)
    {
        Quaternion newRotation = Quaternion.LookRotation(lookObject.transform.position - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, 1f * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Munition"))
        {
            DealDamage(damagePoints);
            Destroy(collision.gameObject);

        }
    }

    void OnDrawGizmos()
    {
        if (canShoot)
        {
            DrawRaycast();
        }
    }

}
