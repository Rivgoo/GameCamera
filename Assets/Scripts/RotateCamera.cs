using UnityEngine;

public class RotateCamera : MonoBehaviour {
	
	[SerializeField]
	[Range(0,20)]
	private float _speedLerp;
	
	[SerializeField]
	[Range(0,100)]
	private float _rotationSpeed;
	
	[SerializeField]
	[Range(0,100)]
	private float _rotationSpeedKey;
	
	[SerializeField]
	[Range(-1,1)]
	private float _sensitivityRotationMous;
	
	[SerializeField]
	private float _zoomSpeed;
	
	[SerializeField]
	private float _maxZoom;
	
	[SerializeField]
	private float _minZoom;
	
//	private Vector3 _rotationStartPosition;
//	private Vector3 _rotationCurrentPosition;
	
	private Vector3 _zoomTarget;
		
	private Quaternion _rotationTarget;
	
	private void Update () 
	{
		RotationMouse();
		RotationKeyDown();
		Zoom();
		ZoomLepr();
	}
	
	private void RotationKeyDown()
	{
			if (Input.GetKey(CameraControlKeys.RotationLeft))
			{	
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y -_rotationSpeedKey, transform.eulerAngles.z);
			}
			else if(Input.GetKey(CameraControlKeys.RotationRight))
			{		
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + _rotationSpeedKey, transform.eulerAngles.z);
			}
			
			RotationSlerp();		
	}
	
	private void RotationMouse()
	{	
		
//		if (Input.GetMouseButtonDown(1))
//		{
//			_rotationStartPosition = Input.mousePosition;
//		}
		
		if (Input.GetMouseButton(1) && !Input.GetKey(CameraControlKeys.RotationLeft) && !Input.GetKey(CameraControlKeys.RotationRight) && !Input.GetMouseButton(0))
		{					
			if (Input.mousePosition.x >= Screen.width - 3)
			{
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + _rotationSpeedKey /2, transform.eulerAngles.z);
			}
			else if (Input.mousePosition.x <= 3)
			{
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y -_rotationSpeedKey/2, transform.eulerAngles.z);
			}
			else if (Input.GetAxis("Mouse X") >= _sensitivityRotationMous)
			{
				_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y + _rotationSpeed, transform.eulerAngles.z);
			}
			else if (Input.GetAxis("Mouse X") <= -_sensitivityRotationMous)
			{
				_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y - _rotationSpeed, transform.eulerAngles.z);
			}
//			else
//			{
//				_rotationCurrentPosition = Input.mousePosition;
//				
//				Vector3 different = _rotationStartPosition - _rotationCurrentPosition;
//				
//				_rotationStartPosition = _rotationCurrentPosition;
//				
//				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y +(-different.x / 5f) *_rotationSpeed,transform.eulerAngles.z);
//			}
			
			
			RotationSlerp();
		}
	}		
	
	private void ZoomLepr()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition,_zoomTarget, Time.deltaTime * _speedLerp);
	}
	
	private void Zoom()
	{
		if (Input.GetKey(KeyCode.R))
		{
			_zoomTarget.z += _zoomSpeed;
		}
		
		if (Input.GetKey(KeyCode.F))
		{
			_zoomTarget.z -= _zoomSpeed;
		}
		
//		if (Input.mouseScrollDelta.y != 0) 
//		{
//			_zoomTarget.z +=  _zoomSpeed * 10 * Input.mouseScrollDelta.y;
//		}
		_zoomTarget.z = Mathf.Clamp(_zoomTarget.z,_minZoom,_maxZoom);
	}
	
	private void RotationSlerp()
	{
		transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, _speedLerp * Time.deltaTime);
	}
	
	private void Start()
	{
		_zoomTarget = transform.localPosition;
	}
}
