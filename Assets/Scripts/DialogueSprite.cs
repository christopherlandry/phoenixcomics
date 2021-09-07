using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSprite : MonoBehaviour
{
    public Actor actor;

    public void showDefault()
    {
        this.GetComponent<SpriteRenderer>().sprite = actor.dialogueSprite;
    }
}
