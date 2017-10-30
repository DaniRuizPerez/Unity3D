using UnityEngine;
using System.Collections;
using UnityEditor;
using System.IO;

public class Testing : Editor 
{
	static void MaterialRoutine(string mode)
	{
		Texture2D tex = new Texture2D(512, 512);
		
		for (int y = 0; y < 512; ++y)
		{
			for (int x = 0; x < 512; ++x) {
				switch(mode){
				case "desert":
					tex.SetPixel(x, y, desertColor(x,y));
					break;
				case "glacier":
					tex.SetPixel(x, y, glacierColor(x,y));
					break;
				default:
					tex.SetPixel(x, y, desertColor(x,y));
					break;
				}

			}
		}

		byte[] pngData = tex.EncodeToPNG();
		
		if(pngData != null)
			File.WriteAllBytes("Assets/Resources/texture.png", pngData);
		
		DestroyImmediate(tex);
		AssetDatabase.Refresh();

	}

	[MenuItem ("TerrainGenerator/Material Routine/Desert")]
	static void MaterialRoutineDesert()
	{
		MaterialRoutine ("desert");
	}

	[MenuItem ("TerrainGenerator/Material Routine/Glacier")]
	static void MaterialRoutineGlacier()
	{
		MaterialRoutine ("glacier");
	}

	static float toRGBFloatValue(float rgb) {
	/*From [0-255] to [0-1]*/
		return (float)(rgb / 255);
	}

	static Color desertColor(int x, int y) {
		//Desert sand: 237, 201, 175
		
		/*float r = toRGBFloatValue(Random.Range (227,247));
		float g = toRGBFloatValue(Random.Range (191,211));
		float b = toRGBFloatValue(Random.Range (165,185));
		Color color = new Color (r, g, b);*/
		float h = (float)22/255;
		float s = (float)67/255;
		float v = (float)Random.Range (105,125)/255;
		return EditorGUIUtility.HSVToRGB (h, s, v);
	}

	static Color glacierColor(int x, int y) {
		//Desert sand: 237, 201, 175
		
		/*float r = toRGBFloatValue(Random.Range (227,247));
		float g = toRGBFloatValue(Random.Range (191,211));
		float b = toRGBFloatValue(Random.Range (165,185));
		Color color = new Color (r, g, b);*/
		float h = (float)185/255;
		float s = (float)Random.Range (10,31)/255;
		float v = (float)255/255;
		return EditorGUIUtility.HSVToRGB (h, s, v);
	}

	static void CreateTerrain(string mode) {
		var tSize = new Vector3 (100, 100, 100);
		var tPos = new Vector3 (0, 0, 0);
		var terrain = GameObject.Find ("GeneratedTerrain");
		if (terrain != null) {
			DestroyImmediate(terrain);
			AssetDatabase.Refresh();
		}
		TerrainData tData = new TerrainData();
		tData.size = tSize;
		var x = tData.heightmapWidth;
		var y = tData.heightmapHeight;
		var hmap = tData.GetHeights (0, 0, x, y);
		switch (mode){
		case "desert":
			hmap = modifierDesert(hmap, x, y);
			break;
		case "glacier":
			hmap = modifierGlacier(hmap, x, y);
			break;
		default:
			hmap = modifierDesert(hmap, x, y);
			break;
		}
		tData.SetHeights (0, 0, hmap);
		tData.splatPrototypes = getSplatPrototypeFromTextureString ("texture");
		GameObject myTerrain = Terrain.CreateTerrainGameObject(tData);
		myTerrain.name = "GeneratedTerrain";
		MaterialRoutine (mode);
		myTerrain.transform.position = tPos;
	}

	[MenuItem ("TerrainGenerator/Create terrain/Glacier")]
	static void CreateTerrainGlacier() {
		CreateTerrain ("glacier");
	}

	[MenuItem ("TerrainGenerator/Create terrain/Desert")]
	static void CreateTerrainDesert() {
		CreateTerrain ("desert");
	}

	static SplatPrototype[] getSplatPrototypeFromTextureString(string texture_name) {
		
		Texture2D tex = Resources.Load(texture_name) as Texture2D;
		var splatProtText = new SplatPrototype ();
		splatProtText.texture = tex;
		SplatPrototype[] spl= {splatProtText};
		return spl;
		
	}

	static void IcePillar(float[,] hmap, int width, float height, float sX, float sY, int cX, int cY){
		for (int i = 0; i < width; i++) {
			for (int j = 0; j < width; j++){
				hmap[(cX - i),(cY - j)] = hmap[(cX - i),(cY - j)] + height + (cX - i)*sX + (cY - j)*sY;
			}		
		}
	}
	
	static float modifier1(int i, int j) {
		return (float)(0.01*(1+(i%2)))+((float)((j%2)*0.01));
	}
	
	static float modifier2(float i, float j) {
		return (float)(0.01 +(float) Random.Range((float)-0.1, (float)0.02));
	}

	static float[,] modifierDesert(float[,] hmap, int x, int y) {
		for (int i = 0; i < x; i++) {
			for (int j = 0; j < y; j++) {
				hmap[i,j] = (float)(Mathf.Cos (Random.Range ((float)(i-j),(float)(i+j)) + Mathf.Sin (Random.Range ((float)(i-j),(float)(i+j))))*0.0105);
			}
		}
		return hmap;
	}

	static float[,] modifierGlacier(float[,] hmap, int x, int y) {
		int pillarWidth = 4;
		for (int i = 4; i < x-4; i+= pillarWidth-1) {
			for (int j = 4; j < y-4; j+= pillarWidth-1) {
				IcePillar(hmap, pillarWidth, ((float) Random.Range(-2, 5)/100), ((float) Random.Range(-2, 2)/1000), ((float) Random.Range(-2, 2)/1000), i, j);
			}
		}
		return hmap;
	}
}