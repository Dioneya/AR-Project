using System;
using System.Collections.Generic;
using UnityEngine;


public class QuestsClass : MonoBehaviour
{
    [Serializable]
    public class QuestsList 
    {
        public List<Quest> data;
    }

    [Serializable]
    public class Quest 
    {
        public int id;
        public string title;
        public string description;
        public int next_id;
        public int prev_id;
        public int type;
        public string created_at;
        public string updated_at;
        public string deleted_at;

        public List<Point> points;
    }

    [Serializable]
    public class Point 
    {
        public int id;
        public string title;
        public string description;
        public int marker_id;

        public string created_at;
        public string updated_at;
        public string deleted_at;

        public List<Pivot> pivots;
    }

    [Serializable]
    public class Pivot 
    {
        public int quest_id;
        public int quest_point_id;
    }

    [Serializable]
    public class Progress 
    {
        public int id;
        public bool is_completed;
        public List<ProgressPoint> points;
    }

    [Serializable]
    public class ProgressPoint
    {
        public int id;
        public string title;
        public int marker_id;
        public bool is_completed;
    }
}
