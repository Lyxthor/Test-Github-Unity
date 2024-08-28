using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator anim;
    private PlayerMovements playerMovements;
    private float cooldownTimer=Mathf.Infinity;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovements = GetComponent<PlayerMovements>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown && playerMovements.canAttack())
            Attack();
        cooldownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        anim.SetTrigger("attack");
        cooldownTimer = 0;
        int index = FindFireballs();
        fireballs[index].transform.position = firePoint.position;
        fireballs[index].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireballs()
    {
        for(int i=0;i<fireballs.Length;i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
