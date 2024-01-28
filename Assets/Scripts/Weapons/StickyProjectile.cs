using UnityEngine;

public class StickyProjectile : Projectile
{
    [SerializeField] private GameObject newObjectCollectTrigger;

    protected override void Start()
    {
        base.Start();
        
        if(newObjectCollectTrigger == null)
            Debug.LogError("No newObjectCollectTrigger found on " + this.gameObject.name + " or its children.");
    }
    
    protected override void OnTriggerEnter(Collider other)
    {

        Debug.Log("Sticky projectile hit player "+other.gameObject.name+"!");
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            //Damage the other object
            damageable.TakeDamage(Damage);
            //Play hit sound
            PlayHitSound();
            //Degrade weapon???
            //Destroy(this.gameObject);
        }
        else
        {
            Instantiate(newObjectCollectTrigger, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }
}