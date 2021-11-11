
using UnityEngine;

namespace Game
{
    // Start is called before the first frame update
    public struct DialogData
    {
        public int id; public string name; public int sprite;
        public string script; 
        public DialogData(int id, string name, int sprite, string script)
        {
            this.id = id; this.name = name; this.sprite = sprite; this.script = script; 
        }

        public void Show()
        {
            Debug.Log("id: " + id + " name: " + name + " sprite: " + sprite + " script: " + script);
        }
        public string GetImageName()
        {
            return "Char" + id.ToString();

        }

    }
}
