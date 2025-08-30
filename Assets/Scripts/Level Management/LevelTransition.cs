using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{
    public int sceneRangeStart = 2;
    public int sceneRangeEnd = 2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(Random.Range(sceneRangeStart, sceneRangeEnd));
            Debug.Log("New scene loaded");
        }
    }
}