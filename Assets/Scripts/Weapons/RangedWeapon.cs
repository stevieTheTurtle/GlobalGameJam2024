using UnityEngine;

public class RangeWeapon : MonoBehaviour, IWeapon, ICollectable
{
    [SerializeField] private GameObject _projectile;
    [SerializeField] private Transform _firePoint; // Position from which projectiles are fired

    public void Attack()
    {
        // Instantiate the projectile at the fire point's position and rotation
        if (_projectile != null && _firePoint != null)
        {
            Instantiate(_projectile, _firePoint.position, _firePoint.rotation);
        }
        else
        {
            Debug.LogError("Projectile or fire point not set for " + gameObject.name);
        }
    }

    public void CollectObjectFor(PlayerManager playerManager)
    {
        // Implementation for collectible behavior
        // Example: Attach this weapon to the player
        this.transform.parent = playerManager.transform;
        this.transform.localPosition = new Vector3(0,0,1f) ; // Adjust as needed
        this.transform.localRotation = Quaternion.identity; // Adjust as needed
    }

    public Transform GetTransform()
    {
        return this.transform;
    }

    public void Release()
    {
        // Implementation for release behavior
        // Example: Detach weapon from player
        this.transform.parent = null;
    }

    void Start()
    {
        // Initialization if needed
    }

    void Update()
    {
        // Update logic if needed
    }
}
