using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

public class SceneScript : MonoBehaviour
{
    private AssetBundle bundle; //the bundle to use for this scene

    public TextBox textbox; //the main text box
    public GameObject actorPrefab; //actor prefab to clone cast members from
    public GameObject background; //background object to paint scene on
    public DialogueSprite actorLeft; //actor on the left dialogue area TODO should we move dialogue to TextBox generally as Dialogue Controller?
    public DialogueSprite actorRight; //actor on the right dialogue area, this one is always mirrored

    //list of actors in the scene. these should be in the asset bundle and loaded during loadScene at start
    Dictionary<string, Actor> cast = new Dictionary<string, Actor>();
    //contains a list of line in the play to execute
    string[] lines;
    //current line number
    int lineNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        loadScene();
        advanceScene(); //start by reading the first line
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            advanceScene();
            //todo should we put a buffer here to prevent speedmashing space?
        }
        //phone control
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                advanceScene();
            }
        }
    }
    //initializes scene, sets backgrounds, cast, etc.
    void loadScene()
    {
        this.bundle = WebLoader.bundle;
        loadScript();
        loadBackground();
        loadActors();
    }

    //loads the file named 'script.txt' into the lines to be read by the engine
    void loadScript()
    {
        
        var source = bundle.LoadAsset<TextAsset>("script");
        var text = source.text;
        lines = text.Split('\n');
    }

    void loadBackground()
    {
        var bg = bundle.LoadAsset<Texture2D>("background");
        background.GetComponent<SpriteRenderer>().sprite = Sprite.Create(bg,
            new Rect(0, 0, bg.width, bg.height), //take entire sprite
            new Vector2(0.5f, 0.5f), //anchor point center
            //100.0f);
            100.0f * Mathf.Min(((float)bg.width / (float)Screen.width), ((float)bg.height / (float)Screen.height))); //scaling
    }

    //initializes the cast object from the cast.txt file
    void loadActors()
    {
        var text = bundle.LoadAsset<TextAsset>("cast").text;
        foreach(var name in text.Split('\n'))
        {

            var actor = Instantiate(actorPrefab); //this will probably be at 0,0 and appear on screen
            var sprite = bundle.LoadAsset<Texture2D>(name.Trim());
            sprite.filterMode = FilterMode.Point;
            actor.GetComponent<Actor>().init(name, sprite);
            
            //var actors = FindObjectsOfType<Actor>();
            cast.Add(name.Trim().ToUpper(), actor.GetComponent<Actor>());
        }
    }

    //execute the script line by line until we hit the end of the scene
    public void advanceScene()
    {
        if (lineNumber == lines.Length)
        {
            textbox.DisplayText("END SCENE");
            SceneManager.LoadScene("MainMenu");
            return;
        }
        executeLine(lines[lineNumber]);
        lineNumber++;
    }

    void executeLine(string line)
    {
        //command line
        if(line[0] == '*')
        {
            var commands = line.Split(' ');
            switch(commands[0])
            {
                case "*enter":
                    moveActor(commands[2], commands[1]);
                    break;
                case "*exit":
                    moveActor(commands[1], "exit");
                    break;
                case "*move":
                    moveActor(commands[2], commands[1]);
                    break;
                case "*sound":
                    playSound(commands[1]);
                    break;
                default:
                    print("UNKNOWN COMMAND: " + line);
                    break;
            }
            //handle multiline commands
            if(commands.Last() == ">>")
            {
                lineNumber++;
                executeLine(lines[lineNumber]);
            }
        } else //dialogue line
        {
            var actor = line.Split(':')[0];
            if (!cast.ContainsKey(actor.Trim().ToUpper()))
            {
                print("could not find actor named " + actor);
                return;
            }
            showActor(actor);
            var speech = line.Split(':')[1];
            textbox.DisplayText(speech, cast[actor.Trim().ToUpper()].position);
        }
    }

    void moveActor(string name, string direction)
    {
        if(!cast.ContainsKey(name.Trim().ToUpper()))
        {
            print("could not find actor named " + name);
            return;
        }
        var actor = cast[name.Trim().ToUpper()];
        switch (direction)
        {
            case "left":
                actor.position = StagePosition.STAGE_LEFT;
                break;
            case "right":
                actor.position = StagePosition.STAGE_RIGHT;
                break;
            case "exit":
                actor.position = StagePosition.OFFSTAGE;
                break;
        }
    }

    void playSound(string sound)
    {
        //TODO
        textbox.DisplayText("**"+sound+"**");
    }

    void showActor(string name)
    {
        if(actorLeft.actor?.name == name || actorRight.actor?.name == name)
        {
            return; //nothing to do, actor already visible
        }
        else
        {
            var actor = cast[name.Trim().ToUpper()];
            if (actor.position == StagePosition.OFFSTAGE) return; //don't show offstage actors
            var acting = (actor.position == StagePosition.STAGE_LEFT) ? actorLeft : actorRight;
            //load actor on left or right according to their current position
            acting.actor = actor;
            //load default actor image
            acting.showDefault();
        }
    }
}
