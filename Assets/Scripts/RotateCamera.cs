
using UnityEngine;

public class RotateCamera : MonoBehaviour {
	
	[SerializeField]
	[Range(0,20)]
	private float _speedLerp;
	
	[SerializeField]
	[Range(0,100)]
	private float _speedRotationKeyDown;
	
	[SerializeField]
	[Range(0,100)]
	private float _speedRotationMous;
	
	[SerializeField]
	[Range(-1,1)]
	private float _sensitivityMousRotationRigth;
	
	[SerializeField]
	[Range(-1,1)]
	private float _sensitivityMousRotationLeft;
	
	private float _speedNormalizeRotation = 100;
		
	private Quaternion _rotationTarget;
	
	private void Update () 
	{
		RotationKeyDown();
		MousRotate();
	}
	
	private void RotationKeyDown()
	{
		if (!Input.GetMouseButton(1)) 
		{
			if (Input.GetKey(KeyCode.Q))
			{
				
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (-_speedRotationKeyDown * Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
			}
			else if(Input.GetKey(KeyCode.E))
			{		
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (_speedRotationKeyDown * Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, _speedLerp * Time.deltaTime);	
		}		
	}
	
	private void MousRotate()
	{	
		if (Input.GetMouseButton(1))
		{		
			if (Input.mousePosition.x >= Screen.width - 2)
			{
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (_speedRotationKeyDown * 2 *Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
			}
			else if (Input.mousePosition.x <= 3)
			{
				_rotationTarget = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + (-_speedRotationKeyDown * 2 * Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
			}
			else
			if (Input.GetAxis("Mouse X") >= _sensitivityMousRotationRigth)
			{
				//_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y + (_speedRotationMous * Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
				_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y + _speedRotationMous, transform.eulerAngles.z);
			}
			else if (Input.GetAxis("Mouse X") <= _sensitivityMousRotationLeft)
			{
				//_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y + (-_speedRotationMous * Time.deltaTime) * _speedNormalizeRotation, transform.eulerAngles.z);
				_rotationTarget = Quaternion.Euler( transform.eulerAngles.x, transform.eulerAngles.y - _speedRotationMous, transform.eulerAngles.z);
			}
		}
		
		transform.rotation = Quaternion.Slerp(transform.rotation, _rotationTarget, _speedLerp * Time.deltaTime);
	}		
}
