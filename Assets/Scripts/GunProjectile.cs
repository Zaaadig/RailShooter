using System.Collections;
using UnityEngine;

public class GunProjectile : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody m_rb;

    [Header("Values")]
    [SerializeField] private float m_speed = 10;
    [SerializeField] private float m_shootingDelay = 0.2f;
    [SerializeField] private float m_chargeDuration = 1.0f;
    [SerializeField] private Vector2 m_speedMultiplier;
    [SerializeField] private Vector2 m_sizeMultiplier;
    [SerializeField] private float m_lifetime = 3;

    private float m_currentChargeTime = 0;
    private bool m_hasReachMaxCharge = false;

    public float CurrentChargeTime { get => m_currentChargeTime; set => m_currentChargeTime = value; }
    public float ChargeDuration { get => m_chargeDuration; set => m_chargeDuration = value; }

    public void Shoot(Vector3 dir) //dir = direction
    {
        float ratio = m_currentChargeTime / m_chargeDuration;
        float speed = Mathf.Lerp(m_speedMultiplier.x, m_speedMultiplier.y, ratio);
        m_rb.AddForce(m_speed * dir, ForceMode.Impulse);
    }

    private IEnumerator C_Lifetime()
    {
        yield return new WaitForSeconds(m_lifetime); // yield return = return pour attendre un certain temps, il est sp�cifique aux coroutines
        DestroyProjectile();
    }

    public void DestroyProjectile()
    {
        Destroy(gameObject);
    }
    public void SetCharge(float newTimer)
    {
        m_currentChargeTime = newTimer;
        float ratio = m_currentChargeTime / m_chargeDuration;
        transform.localScale = Vector3.Lerp(m_sizeMultiplier.x * Vector3.one, m_sizeMultiplier.y * Vector3.one, ratio);
        if (m_currentChargeTime >= m_chargeDuration)
        {
            OnReachMaxCharge();
        }
    }

    private void OnReachMaxCharge()
    {
        m_hasReachMaxCharge = true;
    }

    private void OnImpact()
    {
        DestroyProjectile();
    }

    private void OnCollisionEnter(Collision collision)
    {
        OnImpact();
    }
}
