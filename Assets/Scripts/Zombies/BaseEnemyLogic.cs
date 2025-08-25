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


    public int health;
    public float speed;


    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        player = playerObj.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.stoppingDistance = minDistance;
    }

    private void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.position);

        if (distance < range)
        {
            navMeshAgent.SetDestination(player.position);
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
