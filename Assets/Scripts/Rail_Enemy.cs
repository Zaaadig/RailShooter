using UnityEngine;

public class Rail_Enemy : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float m_health;

    [Header("Effects")]
    [SerializeField] private ParticleSystem m_onDeathVFX;
    public void TakeDamage(float damage, float knockback, Transform damageSource)
    {
        m_health -= damage;

        if (m_health <= 0)
        {
            m_health = 0;
            DestroyEnemy();
        }
    }

    public void DestroyEnemy()
    {
        if (m_onDeathVFX)
        {
            m_onDeathVFX.transform.parent = null;
            m_onDeathVFX.Play();
        }
        Destroy(gameObject);
    }
}