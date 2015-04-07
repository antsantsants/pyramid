using UnityEngine;

public class Crosshair : MonoBehaviour {
	public Texture2D crosshairTex;
	private Rect position;

	void Start() {
		position = new Rect((Screen.width - crosshairTex.width) / 2,
										  	(Screen.height - crosshairTex.height) / 2,
												crosshairTex.width, crosshairTex.height);
	}

	void OnGUI() {
		GUI.DrawTexture(position, crosshairTex);
	}
}
