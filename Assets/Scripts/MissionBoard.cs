using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Makes this editable in the inspector
[System.Serializable]
public class Mission
{
    public string missionName;
    public string sceneName;
    public int minReward;
    public int maxReward;
}

public class MissionBoard : MonoBehaviour
{
    public GameObject panel; // menu panel
    public Mission[] missions;
    public Button[] missionButtons;
    public TMP_Text[] missionLabels; // text on the buttons

    private int[] rolledRewards;

    void Start()
    {
       


        rolledRewards = new int[missions.Length];

        for (int i = 0; i < missionButtons.Length; i++)
        {
            // Copy i into a new variable so every button doesnt use the final value of i
            int index = i;
            missionButtons[i].onClick.AddListener(() => SelectMission(index));
        }

        panel.SetActive(false);
    }

    public void Open()
    {
        Debug.Log("4: Open() entered");

        for (int i = 0; i < missions.Length; i++)
        {
            Mission m = missions[i];

            rolledRewards[i] = Random.Range(m.minReward, m.maxReward + 1);

            missionLabels[i].text = $"{m.missionName}\nReward: ${rolledRewards[i]}";
        }

       

        panel.SetActive (true);
        Time.timeScale = 0f; // pause while menu is open
    }

    public void Close()
    {
        panel.SetActive(false);
        Time.timeScale = 1f;
    }

    void SelectMission(int index)
    {
        GameManager.Instance.Startmission(missions[index].sceneName, rolledRewards[index]);
    }
}
