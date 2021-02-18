using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    float speed = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mask = Resources.Load<GameObject>("Prefabs/Mask");
        Instantiate(mask, this.transform.parent);
        mask.transform.localPosition = new Vector3(-0.44f, -1.27f, -0.017f);
        mask.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }
    private void OnEnable()
    {
        gameObject.transform.localPosition = new Vector3(0,-1f,0);
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime;
    }
}