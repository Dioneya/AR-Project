using UnityEngine;
using UnityEngine.Events;


public class SwipeInput : MonoBehaviour {

	// If the touch is longer than MAX_SWIPE_TIME, we dont consider it a swipe
	public const float MAX_SWIPE_TIME = 0.5f; 
	
	// Factor of the screen width that we consider a swipe
	// 0.17 works well for portrait mode 16:9 phone
	public const float MIN_SWIPE_DISTANCE = 0.17f;
	public bool swipedUp = false;
	public bool swipedDown = false;

	public UnityEvent leftSwipe = new UnityEvent();
	public UnityEvent rightSwipe = new UnityEvent();
	
	public bool debugWithArrowKeys = true;

	Vector2 startPos;
	float startTime;

	public void Update()
	{

		if (debugWithArrowKeys) {
			if(Input.GetKeyDown (KeyCode.RightArrow)) rightSwipe.Invoke();
			if(Input.GetKeyDown (KeyCode.LeftArrow)) leftSwipe.Invoke();
		}

		if(!(Input.touches.Length > 0))	{return;}

		Touch t = Input.GetTouch(0);

		if(t.phase == TouchPhase.Began)
		{
			startPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);
			startTime = Time.time;
		}

		if(t.phase != TouchPhase.Ended) {return;}

		if (Time.time - startTime > MAX_SWIPE_TIME) // press too long
			return;

		Vector2 endPos = new Vector2(t.position.x/(float)Screen.width, t.position.y/(float)Screen.width);

		Vector2 swipe = new Vector2(endPos.x - startPos.x, endPos.y - startPos.y);

		if (swipe.magnitude < MIN_SWIPE_DISTANCE) // Too short swipe
			return;

		if (Mathf.Abs (swipe.x) > Mathf.Abs (swipe.y)) { // Horizontal swipe
			if (swipe.x > 0) {
				rightSwipe.Invoke();
;			}
			else {
				leftSwipe.Invoke();
			}
		}
		else { // Vertical swipe
			if (swipe.y > 0) {
				swipedUp = true;
			}
			else {
				swipedDown = true;
			}
		}
		
		
	}
}