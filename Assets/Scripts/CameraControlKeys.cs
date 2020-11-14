using UnityEngine;

public class CameraControlKeys : MonoBehaviour {

	public static string RotationRight { get{return _rotationRight; } set{ _rotationRight = value;} }
	
	public static string RotationLeft { get{return _rotationLeft; } set{ _rotationLeft = value;} }
	
	public static string Right { get{return _right; } set{ _right = value;} }
	
	public static string Left { get{return _left;}  set {_left = value;} }
	
	public static string Up { get{return _up; } set{ _up = value;} }
	
	public static string Down { get{return _down;} set{ _down = value;} }
	
	private static string _rotationRight;
	
	
	private static  string _rotationLeft;	
	
	private static string _up;
	
	private static  string _down;
	
	private static string _left;
	
	private static string _right;
	
	public static void DefoultValueKey()
	{
		_rotationRight = "e";
		_rotationLeft = "q";
		
		_up = "w";
		_down = "s";
		_left = "a";
		_right = "d";
		
		
	}
	
	private void Start()
	{
		DefoultValueKey();
	}
}
