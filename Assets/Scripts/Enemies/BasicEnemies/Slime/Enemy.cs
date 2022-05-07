using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public IEnemies thisEnemy;
    public LayerMask aggroLayerMask;
    private Collider[] withinAggroColliders;
    private NavMeshAgent navAgent;
    private Player player;
    public bool isGrounded;
    public GameObject SliderHP;
    // Start is called before the first frame update
    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }
    void FixedUpdate()
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(this.transform.position, out hit, 1f, NavMesh.AllAreas))
        {
            isGrounded = true;
            if (!navAgent.isOnNavMesh)
                DungeonGenerator.BakingNav();
        }
        else
        {
            isGrounded = false;
        }
        if (isGrounded)
        {
            withinAggroColliders = Physics.OverlapSphere(this.transform.position, 10, aggroLayerMask);
            if (withinAggroColliders.Length > 0)
            {
                {
                    ChasePlayer(withinAggroColliders[0].GetComponent<Player>());
                    PopUPSlide();
                }
            }
            else
            {
                PopDownSlide();
            }
        }

        switch (thisEnemy.TypeEnemy)
        {
            case (EnemyClass.Basic):
                break;

            case (EnemyClass.MiniBoss):
                break;

            case (EnemyClass.Boss):
                break;

            case (EnemyClass.Special):
                break;
        }
    }
    public void PerformAttack()
    {
        player.Hitted(5);
    }
    public void TakeDamage(int amount)
    {
        thisEnemy.currentHP -= amount;
        if (thisEnemy.currentHP <= 0)
            Die();
    }
    public void Die()
    {
        if(thisEnemy.drops.Length>0)
            DropLoot();
        else { }
        Destroy(gameObject);
    }
    void DropLoot()
    {
        bool instanced = false;
        var i = Random.Range(0, thisEnemy.drops.Length-1);
        GameObject item = thisEnemy.drops[i];
        if (item != null && instanced==false)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            instanced = true;
        }
    }
    void ChasePlayer(Player player)
    {
        navAgent.SetDestination(player.transform.position);
        this.player = player;
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (!IsInvoking("PerformAttack"))
                InvokeRepeating("PerformAttack", .5f, 2f);
        }
        else
        {
            CancelInvoke("PerformAttack");
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Weapon"))
        {
            TakeDamage(collision.gameObject.GetComponent<WeaponCollision>().weapon.Power);
            Debug.Log("Dealt Dmg it has now" + thisEnemy.currentHP);
        }
    }
    void AttackPlayer(Player player)
    {
        this.player = player;
        player.Hitted(thisEnemy.Power);
    }
    void PopUPSlide()
    {
        SliderHP.SetActive(true);
    }
    void PopDownSlide()
    {
        SliderHP.SetActive(false);
    }
    private bool SetDestination(Vector3 targetDestination)
    {
        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetDestination, out hit, 1f, NavMesh.AllAreas))
        {
            navAgent.SetDestination(hit.position);
            return true;
        }
        return false;
    }
}