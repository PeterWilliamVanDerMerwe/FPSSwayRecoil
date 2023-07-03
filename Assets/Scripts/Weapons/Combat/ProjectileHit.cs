using UnityEngine;

public class ProjectileHit : MonoBehaviour
{
    public GameObject impactParticle;
    public Vector3 impactNormal; //Used to rotate impactparticle.
    private void OnCollisionEnter(Collision collision)
    {
        impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, impactNormal));
        Destroy(this.gameObject);
    }
}
