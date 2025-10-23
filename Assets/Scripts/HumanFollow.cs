using UnityEngine;

public class HumanFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
