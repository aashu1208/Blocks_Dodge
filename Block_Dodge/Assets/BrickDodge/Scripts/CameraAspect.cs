using UnityEngine;

public class CameraAspect : MonoBehaviour 
{
	void Start () 
	{
		Camera.main.aspect = 9/16f;
		Screen.sleepTimeout = SleepTimeout.NeverSleep;
        Time.timeScale = 1;
	}
}