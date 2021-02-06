using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TabsItem : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI textMesh;
    [HideInInspector] public Button button;
    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
    }
}
