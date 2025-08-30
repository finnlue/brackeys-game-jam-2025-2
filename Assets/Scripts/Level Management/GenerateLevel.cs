using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GenerateLevel : MonoBehaviour
{
    // Function that can be called to generate a level with furniture, zombie spawners, spawns and exits

    // Step 1: Place furniture in randomized spots
    // Step 2: Place zombie spawners
    // Step 3: Set spawn and exit               TODO
    // Step 4: Place chests with loot           TODO

    // Literal scene names
    [SerializeField] private string[] sceneNames;

    private ObjectSpawner spawner;

    private NavMeshSurface navMesh;

    private void Awake()
    {
        spawner = GetComponent<ObjectSpawner>();

        GameObject navMeshObj = GameObject.Find("NavMesh");
        navMesh = navMeshObj.GetComponent<NavMeshSurface>();
    }

    private void Start()
    {
        spawner.SpawnFurniture();
        navMesh.BuildNavMesh();
        StartCoroutine(spawner.SpawnZombiesCoroutine());
    }


}


