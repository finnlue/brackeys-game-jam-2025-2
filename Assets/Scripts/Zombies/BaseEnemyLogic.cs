using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemyLogic : MonoBehaviour
{
    float distance;
    public Transform player;
    public float range = 100f;
    public float minDistance = 0.5f;
    private NavMeshAgent navMeshAgent;
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
        StartCoroutine(UpdatePathRoutine());
    }

    private IEnumerator UpdatePathRoutine()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, player.position) < range)
            {
                float swarmRadius = Mathf.Clamp(Vector3.Distance(transform.position, player.position) * 0.3f, 0.5f, 2f);
                Vector2 offset = Random.insideUnitCircle * swarmRadius;
                Vector3 currentTarget = player.position + new Vector3(offset.x, 0, offset.y);

                navMeshAgent.SetDestination(currentTarget);
            }

            yield return new WaitForSeconds(Random.Range(0.5f, 1.5f));
        }
    }

    private void Update()
    {
        transform.LookAt(player);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        if (navMeshAgent.velocity.magnitude > 0.1f)
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
