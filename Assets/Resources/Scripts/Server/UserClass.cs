using System.Collections.Generic;
using UnityEngine;
using System;

public class UserClass : MonoBehaviour
{
    [Serializable]
    public class User
    {
        public int id;
        public string email;
        public string name;
        public List<Stat> stats;
        public List<Achievement> achievements;
        public string message;
    }

    [Serializable]
    public class Stat
    {
        public int stat_type;
        public int stat_value;
        public int user_id;
    }

    [Serializable]
    public class Achievement
    {
        public int id;
        public string title;
        public string description;
        public string ach_image_url;
        public int trigger_type;
        public int trigger_value;
        public string prize_description;
        public string prize_image_url;
        public int position;
        public bool is_active;
    }
}
