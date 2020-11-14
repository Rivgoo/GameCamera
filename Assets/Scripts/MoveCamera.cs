using UnityEngine;

public class MoveCamera : MonoBehaviour {
	
	[SerializeField]
	private float _heightCamera;
	
	[SerializeField]
	private float _movementSpeed;
	
	[SerializeField]
	private float _movementRaycastSpeed;
	
	[SerializeField]
	private bool IsRaycast = false;
	
	[SerializeField]
	[Range(0,20)]
	private float _speedLerp;
	
	private Vector3 _dragStartPosition;
	private Vector3 _dragCurrentPosition;
		
	private Transform _camera;
	
	private Vector3 _moveTarget;	
	
	private void Update()
	{	
		MoveMous();
		MoveMouseSidesScreen();
		MoveInputKey();
		
		MoveSlerp();
		
		transform.position = new Vector3(transform.position.x, _heightCamera, transform.position.z); // шоб по У завжди було одне значення
	}
	
	private void Start()
	{
		_moveTarget = transform.position;
		
		_camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Transform>();
	} 
	
	private void MoveMous()
	{	
		if (IsRaycast && !Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.GetMouseButtonDown(0)) 
			{
				var plane = new Plane(Vector3.up, Vector3.zero);
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				float entry;
				
				if (plane.Raycast(ray, out entry))
				{
					_dragStartPosition = ray.GetPoint(entry);
				}
			}
			
			if (Input.GetMouseButton(0)) 
			{
				var plane = new Plane(Vector3.up, Vector3.zero);
				
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				
				float entry;
				
				if (plane.Raycast(ray, out entry))
				{
					_dragCurrentPosition = ray.GetPoint(entry);
				}
				
				_moveTarget = (transform.position + _dragStartPosition - _dragCurrentPosition) * _movementRaycastSpeed;
			}
		}
	} 
	
	private void MoveMouseSidesScreen()
	{
		if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.LeftShift))
		{
			if (Input.mousePosition.y >= Screen.height - 3)
			{
				MoveForward();
			}
			else if (Input.mousePosition.y <= (Screen.height + 3) - Screen.height)
			{
				MoveForward(-1);
			}
			else if (Input.mousePosition.x >= Screen.width - 2)
			{
				MoveRight();
			}
			else if (Input.mousePosition.x <= 3)
			{
				MoveRight(-1);
			}
		}
	} 
	
	private void MoveInputKey()
	{
		if (!Input.GetMouseButton(0)) 
		{
			if (Input.GetKey(CameraControlKeys.Up)|| Input.GetKey(KeyCode.UpArrow))
			{				
				MoveForward();
			}
			
			if (Input.GetKey(CameraControlKeys.Down)|| Input.GetKey(KeyCode.DownArrow))
			{				
				MoveForward(-1);
			}
			
			if (Input.GetKey(CameraControlKeys.Right) || Input.GetKey(KeyCode.RightArrow))
			{
				MoveRight();
			}
			
			if (Input.GetKey(CameraControlKeys.Left) || Input.GetKey(KeyCode.LeftArrow))
			{
				MoveRight(-1);
			}		
		}
	}
	
	private void MoveRight(float value = 1)
	{
		_moveTarget += (_camera.right * value * _movementSpeed);
	}
	
	private void MoveForward(float value = 1)
	{
		_moveTarget += (_camera.forward * value * _movementSpeed);
	}
		
	private void MoveSlerp()
	{
		transform.position = Vector3.Lerp(transform.position, _moveTarget, _speedLerp * Time.fixedDeltaTime);
	} 		
}
