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
    //private DungeonGenerator dungueonGen;
    private Player player;
    public bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        //dungueonGen = GameObject.FindGameObjectWithTag("DungueonGenerator").GetComponent<DungeonGenerator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
                }
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
        DropLoot();
        //CombatEvents.EnemyDied(this);
        //this.Spawner.Respawn();
        Destroy(gameObject);
    }
    void DropLoot()
    {
        //Item item = thisEnemy.GetDrop();
        //if (item != null)
        //  {
        //    PickupItem instance = Instantiate(pickupItem, transform.position, Quaternion.identity);
        //     instance.ItemDrop = item;
        // }
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