using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour
{
    const int dialogueWidth = 75;
    const int dialogueHeight = 112;

    public string actorName;
    public Sprite dialogueSprite;
    public StagePosition position;
    //public Sprite foregroundSprite; 
    //public Sprite backgroundSprite;

    public void init(string name, Texture2D texture)
    {
        this.name = this.actorName = name;
        this.dialogueSprite = Sprite.Create(texture, 
            new Rect(0, 0, texture.width, texture.height), //take entire sprite
            new Vector2(0.5f, 0.5f), //anchor point center
            100.0f * Mathf.Min(dialogueWidth/(float)texture.width, dialogueHeight/(float)texture.height)); //scaling
        this.position = StagePosition.OFFSTAGE;
    }
}

public enum StagePosition
{
    OFFSTAGE,
    STAGE_LEFT,
    STAGE_RIGHT
}
