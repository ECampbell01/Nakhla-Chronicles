using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    Animator anim;
    public float attackCooldown = 0.1f;
    float timer = 0f;

    void Start()
    {
        // Get the components
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer >= attackCooldown)
        {
            if (Input.GetMouseButton(0))
            {
                Stab();
                timer = 0f;
            }
        }
    }

    void Stab()
    {
        anim.SetTrigger("Stab");
    }
}
