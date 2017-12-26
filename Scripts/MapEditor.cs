
using UnityEngine;

public class MapEditor : MonoBehaviour {

    public Texture2D map;
    public ColorToPrefab[] colorMappings;

    void Start() {
        GenerateLevel();
    }

    void GenerateLevel(){
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                GenerateTile(x, y);
            } 
        }
    }

    void GenerateTile(int x, int y)
    {
        Color pixelColor = map.GetPixel(x, y);
        if (pixelColor.a == 0) {
            //pixel is transparent
            return;
        }
        foreach (ColorToPrefab colorMapping in colorMappings) {
            if (colorMapping.color.Equals(pixelColor)) {
                Vector2 position = new Vector2(x, y)*2 - new Vector2(75,10);
                GameObject instance = Instantiate(colorMapping.prefab);
                instance.transform.position = position;
            }
        }
    }

}
