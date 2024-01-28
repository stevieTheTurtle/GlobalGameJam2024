using UnityEngine;

public class StickyProjectile : Projectile
{
    [SerializeField] private GameObject newObjectCollectTrigger;
    [SerializeField] private GameObject newObjectStickyVisual;

    [SerializeField] private float stickyVisualLifetime = 30.0f; // Time in seconds before stickyVisual is destroyed

    protected override void Start()
    {
        base.Start();

        if (newObjectCollectTrigger == null)
            Debug.LogError("No newObjectCollectTrigger found on " + this.gameObject.name);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        Debug.Log("Sticky projectile hit player " + other.gameObject.name + "!");
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            // Damage the other object
            damageable.TakeDamage(Damage);
            // Play hit sound
            // PlayHitSound();

            // Find the 'HoldPoint' tagged child within the hit object
            Transform holdPointTransform = FindDeepChild(other.transform, "HoldPoint");
            if (holdPointTransform != null)
            {
                // Instantiate stickyVisual and set it as a child of 'HoldPoint'
                GameObject stickyVisual = Instantiate(newObjectStickyVisual, holdPointTransform.position, holdPointTransform.rotation);
                stickyVisual.transform.SetParent(holdPointTransform);

                // Adjust local position and rotation relative to 'HoldPoint'
                stickyVisual.transform.localPosition = new Vector3(0, 0, 0);
                stickyVisual.transform.localRotation = Quaternion.Euler(0, 0, 0);

                // Destroy stickyVisual after a specified amount of time
                Destroy(stickyVisual, stickyVisualLifetime);
            }
            else
            {
                Debug.LogError("No GameObject found with the tag 'HoldPoint' within the target");
            }

            // Destroy this object (the projectile)
            Destroy(this.gameObject);
        }
        else
        {
            Instantiate(newObjectCollectTrigger, this.transform.position, this.transform.rotation);
            Destroy(this.gameObject);
        }
    }

    // Helper method to recursively find a child with a given tag
    private Transform FindDeepChild(Transform parent, string tag)
    {
        foreach (Transform child in parent)
        {
            if (child.CompareTag(tag))
            {
                return child;
            }

            Transform found = FindDeepChild(child, tag);
            if (found != null)
                return found;
        }
        return null;
    }
}
