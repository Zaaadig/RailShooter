using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private GunProjectile m_projectilePrefab; //  prefabe du projectile dans les assets du jeu 
    [SerializeField] private Transform m_shootingSpot;
    [SerializeField] private Transform m_rotationPoint;


    [Header("Aim")]
    [SerializeField] private float m_rotationspeed = 10;
    [SerializeField] private LayerMask m_hitLayers;
    private GunProjectile m_currentProjectile; // projectile instantié par le prefab
    private bool m_isCharging = false;
    private Vector3 m_targetPos;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnMouseButtonDown();
        }
        if (Input.GetMouseButtonUp(0))
        {
            OnMouseButtonUp();
        }
        if (m_isCharging && m_currentProjectile)
        {
            float newChargeTime = m_currentProjectile.CurrentChargeTime += Time.deltaTime;
            m_currentProjectile.SetCharge(newChargeTime);
        }

        // faire en sorte que le canon bouge quand on pouge l'endroit ou on vise :
        Vector3 shootDirection = m_targetPos - m_rotationPoint.position; // calculer le nouveau forward pour faire la bonne rotation
        m_rotationPoint.forward = Vector3.RotateTowards(m_rotationPoint.forward, shootDirection.normalized, m_rotationspeed * Time.deltaTime, 10);
    }
    private void FixedUpdate()
    {
        AimHandler();
    }
    private void AimHandler()
    {
        Vector3 mousePos = Input.mousePosition; // récupérer la position pour la position de la caméra
        mousePos.z = Camera.main.transform.position.z;

        // Appliquer le raycast 
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, m_hitLayers))
        // faire en sorte qu'il s'appliquent que sur certaines layers
        {
            m_targetPos = hit.point;
            
        }
        // si on ne touche rien il faut quand même qu'on puisse visé dans le vide et tirer :
        else
        {
           // m_targetPos = ray.direction * 10;
           m_targetPos = ray.GetPoint(20);
        }
    }

    private void OnDrawGizmos()
    // pour voir visuellement si le aimhandler marche en dessinant une sphere à la target position
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_targetPos, 1);
    }

    private void OnMouseButtonDown()
    {
        m_isCharging = true;
        m_currentProjectile = InstantiateProjectile();
    }

    private void OnMouseButtonUp()
    {
        m_isCharging = false;
        ShootProjectile(m_currentProjectile, m_shootingSpot.forward);
        m_currentProjectile = null;

    }
    private GunProjectile InstantiateProjectile()
    // type de variable gunprojectile veut dire que on doit retourner une valeur void = on a pas besoin de résultat,
    // on veut le return de ce projectile ici 
    {
        GunProjectile projectile = Instantiate(m_projectilePrefab, m_shootingSpot);
        projectile.transform.localPosition = Vector3.zero;
        return projectile;
    }
    private void ShootProjectile(GunProjectile projectile, Vector3 dir)
    {
        projectile.transform.parent = null;
        projectile.Shoot(dir);
    }
}