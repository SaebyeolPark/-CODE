using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game;
public class ChatManager : MonoBehaviour
{
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        int dialogsLength = ChatData.instance.dialogs.Length;
        for (int i = 0; i < dialogsLength; i++)
        {
            DialogData dialog = ChatData.instance.dialogs[i];
            GameObject obj = Instantiate(item, transform.position, Quaternion.identity);

            ChatItem curItem = obj.GetComponent<ChatItem>();
            curItem.setUI(dialog.name, dialog.script);
            curItem.id = dialog.id;

            obj.name = i.ToString();         
            curItem.charImage.sprite = Resources.Load<Sprite>(dialog.GetImageName());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
