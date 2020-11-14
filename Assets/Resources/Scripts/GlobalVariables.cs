using System.Collections.Generic;
using UnityEngine;
public class GlobalVariables : MonoBehaviour
{
    public readonly static string link = "https://pro-ar.ru";
    public static UserClass.User user; //инфа о пользователе
    public static TokenClass.TokenResponse tokenResponse; //хранит токен
    public static DictionaryOfInstClass.InstitutionList institutionListInfo; //хранит словарь заведений

    public static InstitutionJsonLoader.InstitutionList institution; // данные текущего института с маркерами + объектами

    public static int checkedID; //переменная хранящая отсканированный ID комнаты

    public static List<GameObject> Images = new List<GameObject>(); // Список хранящий стикеры заведения 

    public static string nameInst; //имя выбранного в меню института
    public static string descInst; //описание выбранного в меню института
    public static int countInst; //кол. стикеров выбранного в меню института


    public static bool isInstChanged = true;
}
