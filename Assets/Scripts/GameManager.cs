using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public int money;
    private int pendingReward; // Reward of missions

    void Awake()
    {
        // Destroys duplicates of this script
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Makes it survive scene changes
    }

    public void Startmission(string sceneName, int reward)
    {
        pendingReward = reward;
        Time.timeScale = 1f; // Unpause so the new scene isnt frozen
        SceneManager.LoadScene(sceneName);
    }

    public void CompleteMission(string returnScene)
    {
        money += pendingReward;
        pendingReward = 0;
        SceneManager.LoadScene(returnScene);
    }
}
