using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;
    public float stabCooldown = 0.1f;
    public float shootCooldown = 0.1f;
    float timer = 0f;

    public Transform aimPoint;
    public GameObject bullet;
    public float bulletVelocity = 10f;

    void Start()
    {
        // Get the components
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        HandleStab();
        HandleShoot();
    }

    void Stab()
    {
        anim.SetTrigger("Stab");
    }

    void Shoot()
    {
        anim.SetTrigger("Shoot");
        SpawnBullet();
    }

    void SpawnBullet()
    {
        GameObject intBullet = Instantiate(bullet, aimPoint.position, aimPoint.rotation);
        intBullet.GetComponent<Rigidbody2D>().AddForce(-aimPoint.up * bulletVelocity, ForceMode2D.Impulse);
    }

    void HandleStab()
    {
        timer += Time.deltaTime;
        if (timer >= stabCooldown)
        {
            if (Input.GetMouseButton(0))
            {
                Stab();
                timer = 0f;
            }
        }
    }

    void HandleShoot()
    {
        timer += Time.deltaTime;
        if (timer >= shootCooldown)
        {
            if (Input.GetMouseButton(1))
            {
                Shoot();
                timer = 0f;
            }
        }
    }
}
