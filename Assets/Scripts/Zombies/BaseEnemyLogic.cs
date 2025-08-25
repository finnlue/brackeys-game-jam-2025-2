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


    public int health;
    public float speed;


    protected virtual void Start()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        sprite = GetComponent<SpriteRenderer>();
        player = playerObj.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();

        navMeshAgent.stoppingDistance = minDistance;
    }

    private void Update()
    {
        transform.LookAt(player);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        distance = Vector3.Distance(this.transform.position, player.position);

        if(distance < range)
        {
            navMeshAgent.SetDestination(player.position);
        } 
    }

    protected void ApplyToAgent()
    {
        navMeshAgent.speed = speed;
    }
}
