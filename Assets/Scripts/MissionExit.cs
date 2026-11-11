using UnityEngine;

public class MissionExit : MonoBehaviour
{
    public string hubScene = "Hub"; // main scene's name

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            GameManager.Instance.CompleteMission(hubScene);
    }
}
