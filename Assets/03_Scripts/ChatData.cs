using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class ChatData : MonoBehaviour
{
    public static ChatData instance;
    public DialogData[] dialogs;
    TextAsset scene1;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Start()
    {
        scene1 = Resources.Load<TextAsset>("Scene1");
        string[] lines = scene1.text.Split('\n');
        dialogs = new DialogData[lines.Length - 2];
        for (int i = 1; i < lines.Length - 1; i++)
        {
            string[] rows = lines[i].Split('\t');
            int id = int.Parse(rows[0]);
            string name = rows[1];
            int sprite = int.Parse(rows[2]);
            string script = rows[3];

            dialogs[i - 1] = new DialogData(id, name, sprite, script);
        }
    }
}
