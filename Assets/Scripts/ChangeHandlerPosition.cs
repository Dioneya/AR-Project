using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeHandlerPosition : MonoBehaviour
{
    [SerializeField] private Transform _grid;
    [SerializeField] private Transform _handler;
    
    private int _gridChildCount;
    private int _childWidth;
    private Vector3 _handlerStartPosition;
    private bool _isLerp = false;
    private int _itemNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        _gridChildCount = _grid.childCount;
        _childWidth = Screen.width / _gridChildCount;
        _handlerStartPosition = _handler.localPosition;
    }

    void Update()
    {
        if (_isLerp) {
            Vector3 newPosition = new Vector3(_itemNumber * _childWidth, _handler.localPosition.y, _handler.localPosition.z);
            _handler.localPosition = Vector3.Lerp(_handler.localPosition, newPosition, 5 * Time.deltaTime);
            if (newPosition == _handler.localPosition)
            {
                _isLerp = false;
            }
        }
    }

    public void MoveHandler(int item) {
        _itemNumber = item;
        _isLerp = true;
    }
}
