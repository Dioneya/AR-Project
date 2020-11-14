using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DictionaryOfInstClass : MonoBehaviour
{
    [Serializable]
    public class InstitutionInfo
    {
        public int id;
        public string title;
    }
    [Serializable]
    public class InstitutionList
    {
        public List<InstitutionInfo> list;
    }
}
