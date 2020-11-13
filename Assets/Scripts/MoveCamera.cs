using UnityEngine;

public class MoveCamera : MonoBehaviour {
	
	[SerializeField]
	private float _heightCamera;
	
	[SerializeField]
	private float _speedMove;
	
	[SerializeField]
	private const float _normilizeMove = 100;
	
	[SerializeField]
	[Range(0,20)]
	private float _speedLerp;
	
	private Vector3 _moveTarget;
	
	[SerializeField]
	private Transform _camera;
	
	private void LateUpdate()
	{							
		MoveInputKey();
		MoveMouseEndScreen();
		transform.position = Vector3.Slerp(transform.position, _moveTarget, _speedLerp * Time.deltaTime);
		
		transform.position = new Vector3(transform.position.x, _heightCamera, transform.position.z);
	}
	
	private void Start()
	{
		_moveTarget = transform.position;
	}
	
	private void MoveMouseEndScreen()
	{
		if (Input.GetMouseButton(0)) 
		{
			if (Input.mousePosition.y >= Screen.height - 3)
			{
				MoveForward(true);
			}
			else if (Input.mousePosition.y <= (Screen.height + 3) - Screen.height)
			{
				MoveForward(true,-1);
			}
			if (Input.mousePosition.x >= Screen.width - 2)
			{
				MoveRight(true);
			}
			else if (Input.mousePosition.x <= 3)
			{
				MoveRight(true,-1);
			}
		}
	}
		
	private void MoveInputKey()
	{
		if (!Input.GetMouseButton(0)) 
		{
			if (Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
			{				
				if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f)
				{
					_moveTarget = new Vector3( ((_camera.forward.x * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime) * _normilizeMove + (_camera.right.x * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z +  (_camera.right.z * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime * _normilizeMove + (_camera.forward.z * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime * _normilizeMove);
				}else
				{
					MoveForward();
				}
				
			}
			else if (Input.GetAxis("Horizontal") >= 0.1f || Input.GetAxis("Horizontal") <= -0.1f)
			{
				if (Input.GetAxis("Vertical") >= 0.1f || Input.GetAxis("Vertical") <= -0.1f)
				{
					_moveTarget = new Vector3( ((_camera.forward.x * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime) * _normilizeMove + (_camera.right.x * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z +  (_camera.right.z * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime * _normilizeMove + (_camera.forward.z * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime * _normilizeMove);
								
				}
				else
				{
					MoveRight();
				}
			}
		}
	}
	
	private void MoveRight(bool isMous = false, float mousMul = 1)
	{
		if (!isMous) 
		{
			_moveTarget = new Vector3( ((_camera.right.x * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime) * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z + (_camera.right.z * Input.GetAxis("Horizontal") * _speedMove) * Time.deltaTime * _normilizeMove);
		}
		else
		{
			_moveTarget = new Vector3( ((_camera.right.x * mousMul * _speedMove) * Time.deltaTime) * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z + (_camera.right.z * mousMul * _speedMove) * Time.deltaTime * _normilizeMove);
		}
	}
	
	private void MoveForward(bool isMous = false, float mousMul = 1)
	{
		if (!isMous) 
		{
			_moveTarget = new Vector3( ((_camera.forward.x * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime) * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z + (_camera.forward.z * Input.GetAxis("Vertical") * _speedMove) * Time.deltaTime * _normilizeMove);
		}
		else
		{
			_moveTarget = new Vector3( ((_camera.forward.x * mousMul * _speedMove) * Time.deltaTime) * _normilizeMove + transform.position.x , transform.position.y,
				                      	   transform.position.z + (_camera.forward.z * mousMul * _speedMove) * Time.deltaTime * _normilizeMove);
		}
	}
}
