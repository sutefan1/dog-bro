using UnityEditor;
using UnityEngine;

public class LevelParserGUI : MonoBehaviour {

    private LevelParser levelParser;

    void Start()
    {
        levelParser = gameObject.GetComponent<LevelParser>();
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(10, 10, 100, 20), "Load Level"))
        {
            string path = EditorUtility.OpenFilePanel("Select Level File", "Assets/Levels", "json");
            print("Load path: " + path);

            if(path != "")
                levelParser.LoadLevel(path);
        }

        if (GUI.Button(new Rect(120, 10, 100, 20), "Save Level"))
        {
            string path = EditorUtility.SaveFilePanel("Select Level File", "Assets/Levels", "dog-bro", "json");
            print("Save path: " + path);

            if (path != "")
                levelParser.SaveLevel(path);
        }
    }

}
