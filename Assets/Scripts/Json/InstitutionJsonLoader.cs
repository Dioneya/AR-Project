﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstitutionJsonLoader : MonoBehaviour
{
    //Классы
    [System.Serializable]
    public class InstitutionObj
    {
        public int id;
        public string title;
        public string description;
        public string image;
        public Type type;
        public bool is_active;
        public GeoCoord geo_coords;
        public List<MarkerJsonLoader.Marker> markers;
    }

    [System.Serializable]
    public class Institution
    {
        public InstitutionObj data;
    }

    [System.Serializable]
    public class InstitutionList
    {
        public List<InstitutionObj> data;  
    }

    [System.Serializable]
    public class Type
    {
        public int value;
        public string key;
        public string description;
    }

    [System.Serializable]
    public class GeoCoord 
    {
        public float latitude;
        public float longitude;
    }
    //Глобальные переменные 
    public string jsonUrl;
    public InstitutionList Institutions;
}
