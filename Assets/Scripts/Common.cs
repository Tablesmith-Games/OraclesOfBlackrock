using UnityEngine;
using System.Collections;

public class Common : MonoBehaviour {
	//float to bool
	public static bool FloatToBool(float Float)
	{
		if (Float < 0f)
			return false;
		else 
		{
			return true;
		}
	}
	//unsign float
	public static float Unsigned(float val)
	{
		if (val < 0f)
			val *= -1;
		return val;
	}
	public static bool ShiftKeysDown()
	{
		if (Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift))
		{
			return true; 
		} else 
			return false;
	}
}
