using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Register : MonoBehaviour
{
    public static void SaveToRegister(string name, string value) {
        PlayerPrefs.SetString(name, value);
        PlayerPrefs.Save();
    }

    public static string LoadValueFromRegister(string name) {
        if (PlayerPrefs.HasKey(name))
        {
            return PlayerPrefs.GetString(name);
        }
        else {
            Debug.Log("Can't load value from register.");
            return "";
        }
    }
}
