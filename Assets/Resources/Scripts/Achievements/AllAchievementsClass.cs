using System;
using System.Collections.Generic;
using UnityEngine;

public class AllAchievementsClass : MonoBehaviour
{
    [Serializable]
    public class AllAchievements
    {
        public int current_page;
        public List<UserClass.Achievement> data;
    }
}
