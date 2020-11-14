using UnityEngine;
using System;

public class AchievementProgressClass : MonoBehaviour
{
    [Serializable]
    public class Achievement
    {
        public UserClass.Achievement achievement;
        public int current_progress;
        public int needed_progress;
        public bool is_complete;
    }
}
