using UnityEngine;

public class BowlZone : MonoBehaviour
{
    [SerializeField] private Transform maskContainer;
    [SerializeField] private StatusBarManager statusBarManager;
    [SerializeField] private float icePerParticle = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IceParticle particle = other.GetComponent<IceParticle>();

        if (particle == null || particle.inCup) return;
        
        particle.inCup = true;
        other.transform.SetParent(maskContainer, true);
        statusBarManager.HealIce(icePerParticle);
    }
}