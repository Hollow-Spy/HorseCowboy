using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowEnemy : MonoBehaviour
{
    [SerializeField] int HP;
    Rigidbody rb;
    HorseMovement Cow;
    [SerializeField] float Knockack;
    [SerializeField] ParticleSystem HitParticles;
    [SerializeField] GameObject CowImpactSound;
    bool dead;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cow = FindObjectOfType<HorseMovement>();
    }

    public void TakeDamage(Vector3 playerpos, Vector3 CollisionPoint)
    {
        HP--;
        HitParticles.gameObject.transform.position = CollisionPoint;
        HitParticles.Play();
        Instantiate(CowImpactSound, transform.position, Quaternion.identity);
      
        if (HP <= 0 && !dead) 
        {
            dead = true;
            
            Physics.IgnoreCollision(Cow.gameObject.GetComponent<Collider>(), GetComponent<Collider>(), true);
            rb.AddForce((transform.position - playerpos).normalized * Knockack * Cow.SpeedMultiplier,ForceMode.Impulse);
            rb.AddForce(Vector3.up * Cow.SpeedMultiplier * Knockack * 2, ForceMode.Impulse);
            rb.AddTorque(CollisionPoint * Knockack * Cow.SpeedMultiplier * 3,ForceMode.Impulse);
            StartCoroutine(SlowingTime());
           
        }
    }
    IEnumerator SlowingTime()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(.12f);
        Time.timeScale = 0.1f;

        float pauseEndTime = Time.realtimeSinceStartup + .15f;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1;
        yield return new WaitForSeconds(.2f);
        GetComponent<Collider>().enabled = true;
        Destroy(this);
    }
    

}
