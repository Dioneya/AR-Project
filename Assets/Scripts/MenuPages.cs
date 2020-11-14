using UnityEngine;

public class MenuPages: MonoBehaviour
{
    [SerializeField] private float speed = 3;
    private float menuItemsX;
    public void ItemsLerp() {
        transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(menuItemsX, transform.localPosition.y, transform.localPosition.z), speed);
    }

    public void MoveCamera(Transform Page) {
        transform.localPosition = new Vector3(0, 0, 0);
        menuItemsX = -Page.localPosition.x;
        ItemsLerp();
    }
}
