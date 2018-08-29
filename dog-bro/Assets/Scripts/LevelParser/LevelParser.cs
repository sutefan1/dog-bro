using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LevelParser : MonoBehaviour {

    public GameObject level;

    public float levelPadding = 3;

    private void OnValidate()
    {
        levelPadding = Mathf.Max(levelPadding, 3);
    }

    public void LoadLevel(string path)
    {
        ResetLevel();



        TextAsset json_ = Resources.Load(path) as TextAsset;
        string json = json_.text;
              
        //using (StreamReader r = new StreamReader(path))
        //{
        //    json = r.ReadToEnd();
        //}

        TileList tilesList = JsonUtility.FromJson<TileList>(json);

        float maxX = -1000;
        float minX = 1000;
        float maxZ = -1000;
        float minZ = 1000;
        GameObject tileGameObject;
        foreach (Tile tile in tilesList.tiles)
        {
            tileGameObject = Instantiate(Resources.Load(tile.name), new Vector3(tile.posX, 0f, tile.posZ), Quaternion.Euler(new Vector3(0f, tile.rotY, 0f))) as GameObject;
            tileGameObject.transform.parent = level.transform;

            if (tileGameObject.transform.position.x > maxX)
                maxX = tileGameObject.transform.position.x;
            if (tileGameObject.transform.position.x < minX)
                minX = tileGameObject.transform.position.x;
            if (tileGameObject.transform.position.z > maxZ)
                maxZ = tileGameObject.transform.position.z;
            if (tileGameObject.transform.position.z < minZ)
                minZ = tileGameObject.transform.position.z;
            if (tile.name == "StartTile")
                gameObject.GetComponent<GameController>().resetPosition = new Vector3(tile.posX, 0, tile.posZ);
        }

        tileGameObject = Instantiate(Resources.Load("Street")) as GameObject;
        tileGameObject.transform.parent = level.transform;

        tileGameObject.transform.localScale = new Vector3(maxX - minX + levelPadding, 0.5f, maxZ - minZ + levelPadding);
        tileGameObject.transform.position = new Vector3((maxX + minX) / 2, -0.25f, (maxZ + minZ) / 2);

        for (float i = minX - levelPadding / 2 + 0.25f; i <= maxX + levelPadding / 2 - 0.25f; i += 0.5f)
        {
            tileGameObject = Instantiate(Resources.Load("CliffTile"), new Vector3(i, 0f, minZ - levelPadding / 2 + 0.25f), Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject;
            tileGameObject.transform.parent = level.transform;

            tileGameObject = Instantiate(Resources.Load("CliffTile"), new Vector3(i, 0f, maxZ + levelPadding / 2 - 0.25f), Quaternion.Euler(new Vector3(0f, 90f, 0f))) as GameObject;
            tileGameObject.transform.parent = level.transform;
        }
        
        for (float i = minZ - levelPadding / 2 + 0.75f; i <= maxZ + levelPadding / 2 - 0.75f; i += 0.5f)
        {
            tileGameObject = Instantiate(Resources.Load("CliffTile"), new Vector3(minX - levelPadding / 2 + 0.25f, 0f, i), Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
            tileGameObject.transform.parent = level.transform;

            tileGameObject = Instantiate(Resources.Load("CliffTile"), new Vector3(maxX + levelPadding / 2 - 0.25f, 0f, i), Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
            tileGameObject.transform.parent = level.transform;
        }
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
            if (child.gameObject.tag != "Street" && child.gameObject.tag != "Untagged"
                && child.gameObject.tag != "CliffTile")
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

    [System.Serializable]
    public class Tile
    {
        public string name;
        public float posX;
        public float posZ;
        public float rotY;
    }

    [System.Serializable]
    public class TileList
    {
        public List<Tile> tiles;
    }

}
