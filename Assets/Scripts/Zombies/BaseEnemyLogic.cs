using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyLogic : MonoBehaviour
{
    float distance;
    public Transform player;
    public float range = 20f;
    public float minDistance = 0.5f;
    public NavMeshAgent navMeshAgent;
    SpriteRenderer sprite;
    Animator anim;


    public int health;
    public float speed;


    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
        sprite = GetComponent<SpriteRenderer>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        navMeshAgent.stoppingDistance = minDistance;
    }

    private void Update()
    {
        transform.LookAt(player);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        distance = Vector3.Distance(this.transform.position, player.position);

        if (distance < range)
        {
            navMeshAgent.SetDestination(player.position);
        }

        //Debug.Log(navMeshAgent.velocity.magnitude);

        if(navMeshAgent.velocity.magnitude > 0.1f)
        {
            anim.SetInteger("Mode", 1);
        } 
        else
        {
            anim.SetInteger("Mode", 0);
        }
    }

    protected void ApplyToAgent()
    {
        navMeshAgent.speed = speed;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(gameObject.name +" took " + damage + "damage. HP : "+ health);
        if (health <= 0)
            Die();

    }
    void Die()
    {
        Debug.Log("Killed" + gameObject.name);
    }
}
