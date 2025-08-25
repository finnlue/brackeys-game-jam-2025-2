using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform cameraPosistion;

    void Update()
    {
        transform.position = cameraPosistion.position; 
    }
}
