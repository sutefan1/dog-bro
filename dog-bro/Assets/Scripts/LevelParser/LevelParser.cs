using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class LevelParser : MonoBehaviour {

    public GameObject level;

    public void LoadLevel(string path)
    {
        ResetLevel();

        string json;

        using (StreamReader r = new StreamReader(path))
        {
            json = r.ReadToEnd();
        }

        List<Tile> tiles = JsonConvert.DeserializeObject<List<Tile>>(json);

        float maxX = 0;
        float minX = 0;
        float maxZ = 0;
        float minZ = 0;
        GameObject gameObject;
        foreach (Tile tile in tiles)
        {
            gameObject = Instantiate(Resources.Load(tile.name), new Vector3(tile.posX, 0f, tile.posZ), Quaternion.Euler(new Vector3(0f, tile.rotY, 0f))) as GameObject;
            gameObject.transform.parent = level.transform;

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
        gameObject.transform.parent = level.transform;

        gameObject.transform.localScale = new Vector3(maxX - minX + 5, 0.5f, maxZ - minZ + 5);
        gameObject.transform.position = new Vector3((maxX + minX) / 2, -0.25f, (maxZ + minZ) / 2);
    }

    public void ResetLevel()
    {
        foreach (Transform child in level.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void SaveLevel(string path)
    {
        List<Tile> tiles = new List<Tile>();

        foreach (Transform child in level.transform)
        {
            if (child.gameObject.tag != "Street" && child.gameObject.tag != "Untagged")
            {
                Tile tile = new Tile();
                tile.name = child.gameObject.tag;
                tile.posX = child.position.x;
                tile.posZ = child.position.z;
                tile.rotY = child.rotation.eulerAngles.y;
                tiles.Add(tile);
            }
        }

        using (TextWriter file = File.CreateText(path))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, tiles);
        }
    }

    public class Tile
    {
        public string name;
        public float posX;
        public float posZ;
        public float rotY;
    }

}
