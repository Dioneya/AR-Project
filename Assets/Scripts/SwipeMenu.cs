using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SwipeMenu : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    [SerializeField] private ChangeMenu _changeMenu;
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.delta.x) > Mathf.Abs(eventData.delta.y)) {

            if (eventData.delta.x > 0)
            {
                //Debug.Log("Right Swipe"); 
                _changeMenu.NextPage();
            }

            else { 
                //Debug.Log("Left"); 
                _changeMenu.PrevPage();
            }

            //green.position += new Vector3(eventData.delta.x, 0, 0);

        }

        else{

            if (eventData.delta.y > 0) Debug.Log("Up");

            else Debug.Log("Down");


        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
