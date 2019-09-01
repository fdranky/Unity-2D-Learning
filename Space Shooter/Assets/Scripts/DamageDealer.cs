using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] int damage = 100;
    [SerializeField] bool enemyProjectile;
    public int Damage { get { return damage; } }

    public void Hit()
    {
        Destroy(gameObject);
    }
}
