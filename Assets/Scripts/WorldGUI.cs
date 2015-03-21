using UnityEngine;
using System.Collections;

public class WorldGUI : MonoBehaviour {

	[HideInInspector] public GameObject GUICamera;

	private Resolution ScreenResolution;
	public LayerMask GUICameraLayerMask;
	//public SpriteManager spriteManager;

	public static WorldGUI Instance;

	void Start()
	{
				Instance = this;

				//Resolution
				Resolution currentResolution = Screen.currentResolution;

				if (Application.isEditor)
						ScreenResolution = Screen.resolutions [0];
				else
						ScreenResolution = currentResolution;

				//Set Resolution
				Screen.SetResolution (ScreenResolution.width, ScreenResolution.height, true);

				//GUI Camera setup
				GUICamera = new GameObject ("GUICamera");
				GUICamera.AddComponent <Camera>();

				Camera guiCamera = GUICamera.GetComponent<Camera>();

				guiCamera.cullingMask = GUICameraLayerMask;
				guiCamera.name = "GUICamera";
				guiCamera.orthographicSize = ScreenResolution.height / 2;
				guiCamera.orthographic = true;
				guiCamera.nearClipPlane = 0.3f;
				guiCamera.farClipPlane = 50f;
				guiCamera.clearFlags = CameraClearFlags.Depth;
				guiCamera.depth = 1;
				guiCamera.rect = new Rect (0, 0, 1, 1);
				guiCamera.renderingPath = RenderingPath.UsePlayerSettings;
				guiCamera.targetTexture = null;
				guiCamera.hdr = false;
		}
}
