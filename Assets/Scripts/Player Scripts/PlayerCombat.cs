// Contributions: Ryan Lebato and Ethan Campbell

using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;
    public float stabCooldown = 0.1f;
    public float shootCooldown = 0.1f;
    float timer = 0f;
    bool isAttacking = false;
    float attackDuration = 0.3f;
    float attackTimer = 0f;

    public GameObject melee;
    public Transform aimPoint;
    public GameObject bullet;
    public float bulletVelocity = 10f;
    private CameraSwitcher cameraSwitcher;

    void Start()
    {
        anim = GetComponent<Animator>();
        cameraSwitcher = FindObjectOfType<CameraSwitcher>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMeleeTimer();
        HandleStab();
        HandleShoot();
    }

    void Stab()
    {
        //if (!isAttacking && !cameraSwitcher.isPauseMenuActive)
        if (!isAttacking)
        {
            melee.SetActive(true);
            isAttacking = true;
            anim.SetTrigger("Stab");
        }
    }

    void CheckMeleeTimer()
    {
        if (isAttacking)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackDuration) 
            {
                attackTimer = 0;
                isAttacking = false;
                melee.SetActive(false);
            }
        }
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
