using UnityEditor;
using UnityEngine;

public class EzSS_Style : Editor    {
	
	public static GUIStyle customStyle;
	public static GUIStyle originalStyle;

	public static Color32 uiLineColor = new Color32(89, 89, 89, 255);

	public static void DrawHeader(string title){
		// Configurate the style to mach the foldout one
		customStyle = EditorStyles.label;
		originalStyle = customStyle;
		customStyle.fontStyle = FontStyle.Bold;
		// Draw the header
		EditorGUILayout.LabelField(title, customStyle);
		// Revert the style
		customStyle = originalStyle;
	}

	public static bool DrawFoldoutHeader(string title, bool show){
		// Configurate the style to mach the foldout one
		customStyle = EditorStyles.foldout;
		originalStyle = customStyle;
		customStyle.fontStyle = FontStyle.Bold;
		// Draw the header
		show = EditorGUILayout.Foldout(show, title, customStyle);
		// Revert the style
		customStyle = originalStyle;
		return show;
	}

	public static void DrawUILine(Color color, int thickness = 1, int padding = 10){
		Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding + thickness));
		r.height = thickness;
		r.y += padding / 2;
		r.x -= 2;
		r.width += 6;
		EditorGUI.DrawRect(r, color);
	}

	public static void DrawFooter(){
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("SMG Assets", GUILayout.Height(20)))
			Application.OpenURL("https://www.assetstore.unity3d.com/en/#!/search/page=1/sortby=popularity/query=publisher:11524");
		if (GUILayout.Button("SoloMid Games", GUILayout.Height(20)))
			Application.OpenURL("https://solomidgames.com");
		EditorGUILayout.EndHorizontal();

		if (GUILayout.Button("Support", GUILayout.Height(20)))
			Application.OpenURL("mailto:help@solomidgames.com");
		if (GUILayout.Button("DONATE", GUILayout.Height(40)))
			Application.OpenURL("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=hello%40solomidgames%2ecom&lc=US&item_name=SoloMid%20Games&item_number=donation%2dunity%2dassetstore&currency_code=USD&bn=PP%2dDonationsBF%3abtn_donate_LG%2egif%3aNonHosted");
	}
}