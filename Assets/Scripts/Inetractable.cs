using UnityEngine;

public class Inetractable : MonoBehaviour
{
    public MissionBoard missionBoard;

    public void Interact()
    {
        missionBoard.Open();
    }
}
