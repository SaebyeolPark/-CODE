using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ChatItem : MonoBehaviour
{
    public Image charImage;
    public Text charName;
    public Text charScript;
    public int id;

    public void setUI(string charName, string charScript)
    {
        this.charName.text = charName;
        this.charScript.text = charScript;
    }
  
}
