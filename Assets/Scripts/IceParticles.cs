using UnityEngine;

public class IceParticle : MonoBehaviour
{
    public bool inCup;
    public float killY;

    private void Update()
    {
        if (!inCup && transform.position.y < killY)
        {
            Destroy(gameObject);
        }
    }
}