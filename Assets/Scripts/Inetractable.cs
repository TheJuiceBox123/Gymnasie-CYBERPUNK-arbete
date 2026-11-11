using UnityEngine;

public class Inetractable : MonoBehaviour
{
    public MissionBoard missionBoard;

    public void Interact()
    {
        Debug.Log("1: Interact() reached", this);

        if (missionBoard == null)
        {
            Debug.LogError("2: missionBoard is NOT assigned");
            return;
        }

        Debug.Log("2: calling Open on " + missionBoard.name, missionBoard);
        missionBoard.Open();
        Debug.Log("3: Open() returned");
    }
}
