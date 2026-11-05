using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MissionBoard : MonoBehaviour
{
    // Makes this editable in the inspector
    [System.Serializable]
    public class Mission
    {
        public string missionName;
        public string sceneName;
    }

    public void Open()
    {

    }
}
