using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class LevelParserGUI : MonoBehaviour {

    private List<GameObject> gameObjects = new List<GameObject>();

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 20), "Load Level"))
        {
            string path = EditorUtility.OpenFilePanel("Select Level File", "Levels", "json");
            print("Load path: " + path);
            LoadLevel(path);
        }

        if (GUI.Button(new Rect(120, 10, 100, 20), "Save Level"))
        {
            string path = EditorUtility.SaveFilePanel("Select Level File", "Levels", "dog-bro", "json");
            print("Save path: " + path);
            SaveLevel(path);
        }
    }

    private void LoadLevel(string path)
    {
        ResetLevel();
        
        using (StreamReader r = new StreamReader(path))
        {
            string json = r.ReadToEnd();
            List<Tile> tiles = JsonConvert.DeserializeObject<List<Tile>>(json);

            float maxX = 0;
            float minX = 0;
            float maxZ = 0;
            float minZ = 0;
            GameObject gameObject;
            foreach (Tile tile in tiles)
            {
                gameObject = Instantiate(Resources.Load(tile.name), new Vector3(tile.posX, 0f, tile.posZ), Quaternion.Euler(new Vector3(0f, tile.rotY, 0f))) as GameObject;
                gameObjects.Add(gameObject);

                if (gameObject.transform.position.x > maxX)
                    maxX = gameObject.transform.position.x;
                if (gameObject.transform.position.x < minX)
                    minX = gameObject.transform.position.x;
                if (gameObject.transform.position.z > maxZ)
                    maxZ = gameObject.transform.position.z;
                if (gameObject.transform.position.z < minZ)
                    minZ = gameObject.transform.position.z;
            }

            gameObject = Instantiate(Resources.Load("Street")) as GameObject;
            gameObjects.Add(gameObject);

            gameObject.transform.localScale = new Vector3(maxX - minX + 2, 0.5f, maxZ - minZ + 2);
            gameObject.transform.position = new Vector3((maxX + minX) / 2, -0.25f, (maxZ + minZ) / 2);
        }
    }

    private void ResetLevel()
    {
        foreach (GameObject go in gameObjects)
        {
            GameObject.Destroy(go);
        }

        gameObjects = new List<GameObject>();
    }

    private void SaveLevel(string path)
    {

    }

    public class Tile
    {
        public string name;
        public float posX;
        public float posZ;
        public float rotY;
    }

}
