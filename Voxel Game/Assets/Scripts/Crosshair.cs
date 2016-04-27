using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour
{
	public bool drawCrosshair = true; //draws the crosshair instead of making a GUI
	Color crosshairColor = Color.white; 
	public float width = 2;
	public float height = 3; 
	
	[System.Serializable] //if the field isn't serializable, it will be null when switching from editor to play mode
	
	public class spreading
	{
		public float sSpread = 20;
		public float maxSpread = 60;
		public float minSpread = 20;
		public float spreadPerSecond = 30;
		public float decreasePerSecond = 25;
	}
	
	public spreading spread = new spreading();
	
	Texture2D tex; //Creates a new empty texture
	float newHeight;
	GUIStyle lineStyle; //GUIStyle allows adjustment of the layout contained in the box
	

	void Awake () {
		tex = new Texture2D(1, 1); //Constructs a new vector with given x and y components
		lineStyle = new GUIStyle(); 
		lineStyle.normal.background = tex;
		if (this.transform.parent.gameObject.tag == "Cerulean") 
		{
			float r = 0f / 255;
			float g = 123f / 255;
			float b = 167f / 255;
			crosshairColor = new Color(r, g, b);
		}
		else if (this.transform.parent.gameObject.tag == "Burgundy")
		{
			float r = 125f / 255;
			float g = 0f / 255;
			float b = 29f / 255;
			crosshairColor = new Color(r, g, b);
		}
		SetColor (tex, crosshairColor);
	}
	
	void OnGUI () {
		Vector2 centerPoint = new Vector2(Screen.width / 2, Screen.height / 2); // Screen -> Access to display information
		float screenRatio = Screen.height / 100;
		
		newHeight = height * screenRatio;
		
		if (drawCrosshair) {
			GUI.Box(new Rect(centerPoint.x - (width / 2), centerPoint.y - (newHeight + spread.sSpread), width, newHeight), GUIContent.none, lineStyle);
			GUI.Box(new Rect(centerPoint.x - (width / 2), (centerPoint.y + spread.sSpread), width, newHeight), GUIContent.none, lineStyle);
			GUI.Box(new Rect((centerPoint.x + spread.sSpread), (centerPoint.y - (width / 2)), newHeight, width), GUIContent.none, lineStyle);
			GUI.Box(new Rect(centerPoint.x - (newHeight + spread.sSpread), (centerPoint.y - (width / 2)), newHeight, width), GUIContent.none, lineStyle);
			//GUIContent defines what to render, while GUIStyle shows how to render it
			//GuiContent.none -> empty content
		}
		
		if (Input.GetKey(KeyCode.Mouse0)) { //First primary mouse button 
			spread.sSpread += spread.spreadPerSecond * Time.deltaTime;
			Fire();
		}
		
		spread.sSpread -= spread.decreasePerSecond * Time.deltaTime;
		spread.sSpread = Mathf.Clamp(spread.sSpread, spread.minSpread, spread.maxSpread);
		//Mathf.Clamp clamps a value between minimum and maximum float value 
	}
	
	void Fire() { }
	
	void SetColor(Texture2D myTexture, Color myColor) {
		for (int y = 0; y < myTexture.height; y++) {
			for (int x = 0; x < myTexture.width; x++)
				myTexture.SetPixel(x, y, myColor); //SetPixels works with Apply
			myTexture.Apply(); //Apply -> upload the changed pixels to the graphics card
		}
	}
}

