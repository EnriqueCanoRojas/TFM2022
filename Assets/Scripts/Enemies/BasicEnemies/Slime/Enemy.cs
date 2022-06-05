using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer m_SpriteRenderer;
    private Color m_NewColor;
    float m_Red, m_Blue, m_Green;


    public IEnemies thisEnemy;
    public LayerMask aggroLayerMask;
    private Collider[] withinAggroColliders;
    private NavMeshAgent navAgent;
    private Player player;
    public bool hitted;
    public bool isGrounded;
    public bool colorchosed;
    public GameObject SliderHP;
    public int MommentHP;

    public ToggleVariable EndGame;

    // Start is called before the first frame update
    void Start()
    {
        colorchosed = false;
        navAgent = GetComponent<NavMeshAgent>();
        hitted = false;
        SelectColor();
        //m_SpriteRenderer.color = Color.blue;
    }
    void FixedUpdate()
    {
        MommentHP = thisEnemy.currentHP;
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
        if (isGrounded && !hitted)
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
        if (hitted == true)
        {
            StartCoroutine(RecoveryCD());
        }
        switch (thisEnemy.TypeEnemy)
        {
            case (EnemyClass.Basic):
                break;

            case (EnemyClass.MiniBoss):
                break;

            case (EnemyClass.Boss):
                m_SpriteRenderer.color = Color.red;
                break;

            case (EnemyClass.Special):
                break;
        }
    }
    public IEnumerator RecoveryCD()
    {
        yield return new WaitForSeconds(2);
        hitted = false;
    }
    public void SelectColor()
    {
        m_SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (thisEnemy.TypeEnemy == EnemyClass.Basic && colorchosed == false)
        {
            var Arraylist = new Color[] { Color.blue, Color.green, Color.black , Color.cyan , Color.magenta, Color.grey, Color.white, Color.yellow };
            var l = 0;
            l = Random.Range(0, Arraylist.Length);
            m_SpriteRenderer.color = Arraylist[l];
            colorchosed = true;
        }
    }
    public void PerformAttack()
    {
        player.Hitted(thisEnemy.Power);
    }
    public void TakeDamage(int amount)
    {
        hitted = true;
        Debug.Log("BeingHitted");
        thisEnemy.currentHP -= amount;
        if (thisEnemy.currentHP <= 0)
        {
            if (EndGame!=null)
            { 
                EndGame.RuntimeToogle = true;
            }
         Die();
            
        }
    }
    public void Die()
    {
        if(thisEnemy.drops.Length>0)
        {
            DropLoot();
            
        }
        else { }
        Destroy(gameObject);
    }
    void DropLoot()
    {
        bool instanced = false;
        var i = Random.Range(0, thisEnemy.drops.Length);
        GameObject item = thisEnemy.drops[i];
        if (item != null && instanced==false)
        {
            Instantiate(item, transform.position, Quaternion.identity);
            if (thisEnemy.isSpecialDrop)
                Instantiate(thisEnemy.specialDrop, transform.position, Quaternion.identity);
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
    void OnTriggerEnter(Collider collision)
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