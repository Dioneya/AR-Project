using System.Collections.Generic;
using UnityEngine;
public class GlobalVariables : MonoBehaviour
{
    public readonly static string link = "https://pro-ar.ru";
    //public static DictionaryOfInstClass.InstitutionList institutionListInfo; //хранит словарь заведений

    public static InstitutionJsonLoader.Institution institution; // данные текущего института с маркерами + объектами

    public static int checkedID = 1; //переменная хранящая отсканированный ID комнаты

    public static List<GameObject> Images = new List<GameObject>(); // Список хранящий стикеры заведения 

    public static bool isInstChanged = true;
}
