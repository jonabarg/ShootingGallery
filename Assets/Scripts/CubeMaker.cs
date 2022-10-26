using System.Collections;//dependency
using System.Collections.Generic;//dependency
using UnityEngine;//dependency
using UnityEngine.UI;//dependency
using TMPro;//dependency

public class CubeMaker : MonoBehaviour { //begins class definition
    [SerializeField]//set next variable to be definable from Unity inspector
    protected Transform prefab;//the base for each cube
    protected List<Vector3> positions = new List<Vector3>();//stores the position of each cube
    protected Vector3 position;//used to control position of cubes, a 'handle'
    protected Transform target;//used to control transform of cubes, another 'handle'
    protected List<Transform> targets = new List<Transform>();//stores the transform of each cube
    protected List<int> targetsHit = new List<int>();//currently unused, in previous versions this tracked which cubes had been hit by the player already

    [SerializeField]//set next variable to be definable from Unity inspector
    protected TextMeshProUGUI textBox;//this is the textbox onscreen that displays the score and cubes missed
    public int _score = 0;//this is used to track the score
    public int missed = 0;//this is used to track the number of cubes missed

    protected void makeCube(int index) {//this begins the definition of a function named makeCube. It takes an index as input
        position = new Vector3(Random.Range(-10, 10), Random.Range(8, 12), Random.Range(-2, 2));//this sets position to a random position
        if (positions.Contains(position)) {//if the list of all cube positions includes the new random position, run the following code
            makeCube(index);//calls makecube(again) using the index from the input of this function
        }//this bracket closes the if statement
        else {//if the condition of the if statement is not true, the following code is executed
            positions.Add(position);//this adds the new random position to the list of all positions
            target = Instantiate(prefab);// this creates a new cube
            target.position = positions[index];//this sets the new cube's position to the one just created and assigned
            target.name = "" + index;//this sets the cube's name to it's index in the lists that track it
            targets.Add(target);//this adds the new cube's transform to the list of cube transforms
        }//this bracket closes the else statement
    }//this bracket closes the makeCube function statement

    protected void moveCube(int index) {//this begins the definition of a function named moveCube. It takes an index as input
        position = new Vector3(Random.Range(-10, 10), Random.Range(8, 12), Random.Range(-2, 2));//this sets position to a random position
        if (positions.Contains(position)) {//if the list of all cube positions includes the new random position, run the following code
            moveCube(index);//calls movecube (again) using the index from the input of this function
        }//this bracket closes the if statement
        else {
            positions[index] = position;//this sets the position at the index to the new random position
            target = targets[index];//this sets the current target to the target at the given index in the list of targets
            target.position = positions[index];//this sets the position of the target to the new random position
            targets[index] = target;//this sets the target at the current index in the list of targets to the current target.
        }//this bracket closes the else statement
    }//this bracket closes the moveCube function statement

    protected void Start() {//this begins the definition of a function named Start. It takes no input
        for (int i = 0; i < 10; i++) {//this runs the following code 10 times
            makeCube(i);//calls makeCube using i as the input
        }//this bracket closes the for loop
    }//this bracket closes the Start function statement

    [SerializeField]//set next variable to be definable from Unity inspector
    protected Camera cam;//this is the main (and only) camera

    public enum GameState {//this begins the definition of an enumerator named GameState. It serves as the core of a state machine
        Playing,//this is used to determine when the game is being played. It is the default
        Paused,//this is used to tell that the game is paused
        GameOver//this is used to indicate when the game has been lost
    };//this closes the enumerator definition

    public GameState state;//an instance of GameState

    private void play() {//this begins the definition of a function named play. It takes no input, is only useable in this class, and returns nothing
        state = GameState.Playing;//this sets the current game state to playing
    }//this bracket ends the function definition

    private void pause() {//this begins the definition of a function named pause. It takes no input, is only useable in this class, and returns nothing
        state = GameState.Paused;//this sets the current game state to paused
    }//this bracket ends the function definition

    private void end() {//this begins the definition of a function named end. It takes no input, is only useable in this class, and returns nothing
        state = GameState.GameOver;//this sets the current game state to gameover
    }//this bracket ends the function definition
    [SerializeField]//set next variable to be definable from Unity inspector
    protected RectTransform EndText;//this is the text displayed upon gameover

    protected void Update() {//this begins the definition of the Update function, also known as 'the game loop' and runs once each frame
        switch (state) {//switch case based on the state instance of the GameState enumerator
            case GameState.Playing://in the case that state is set to playing
                if (missed >= 50) {//if the player has missed 50 (or more) cubes, run the following code
                    end();//calls the end function, which changes the current game state to GameOver
                }//this bracket closes the if statement
                if (Input.GetKeyDown("escape")) {//if the escape key has just been pressed, run the following code
                    pause();//calls the pause function, which changes the current game state to paused
                }//this bracket closes the if statement
                if (Input.GetMouseButtonDown(0)) {//if the left mouse button has just been pressed, run the following code
                    Vector2 mousePos = Input.mousePosition;//creates a variable to hold the mouses current position and assigns the mouses current position to it
                    Ray ray = cam.ScreenPointToRay(mousePos);//creates a ray that points towards the mouse cursor's position

                    RaycastHit rch;//this will be used as a 'handle' for any object the ray hits

                    if (Physics.Raycast(ray, out rch)) {//if the ray hits an object, run the following code
                        moveCube(int.Parse(rch.transform.name));//this assumes the object is a cube, and uses the object's name as an integer as the input for the call to the moveCube function
                        _score++;//add one to the score tracker variable
                    }//this bracket closes the if statement
                }//this bracket closes the if statement
                textBox.text = "Score: " + _score + "\nMissed: " + missed;//this updates the score and cubes missed tracker on the hud with the current score and cubes missed
                break;//this ends the definition of the "Playing" case
            case GameState.Paused://in the case that state is set to Paused
                foreach (var t in targets) {//for every item in targets list, the following code is executed
                    (t.gameObject).GetComponent<Rigidbody>().isKinematic = true;//this sets the current item to be kinematic (makes the object tied to it not move)
                }//this bracket closes the definition of the for each loop
                GameObject.Find("Reset").GetComponent<Button>().interactable = false;//this makes the reset button un-interactable without fully disabling it

                if (Input.GetKeyDown("escape")) {//if the escape key has just been pressed, run the following code
                    play();//calls the play function, which changes the current game state to Playing
                    foreach (var t in targets) {//for every item in targets list, the following code is executed
                        (t.gameObject).GetComponent<Rigidbody>().isKinematic = false;//this sets the current item to be non-kinematic (makes the object tied to it able to move)
                    }//this bracket closes the definition of the for each loop
                    GameObject.Find("Reset").GetComponent<Button>().interactable = true;//this makes the reset button interactable
                }//this bracket closes the if statement
                break;//this ends the definition of the "Paused" case
            case GameState.GameOver://in the case that state is set to GameOver
                GameObject.Find("Reset").GetComponent<Button>().interactable = false;//this makes the reset button un-interactable without fully disabling it
                EndText.gameObject.SetActive(true);//this activates a textbox containing the string "Game Over" which indicates that the player has lost
                break;//this ends the definition of the "GameOver" case
        }//this bracket closes the switch case definition
    }//this bracket closes the Update function definition

    private void OnTriggerEnter(Collider collision) {//this begins the definition of the OnTriggerEnter function. It runs when an object enters the trigger
        if (state == GameState.Playing) {//if the current game state is 'Playing', the following code is executed
            moveCube(int.Parse(collision.transform.name));//executes the moveCube function on the object that entered the trigger, effectively reseting it
            missed++;//adds one to the count of cubes the player has missed
        }//this bracket closes the if statement
        else {//if the condition of the previous if statement is not true, the following code is executed.
            Destroy(collision);//the object that entered the trigger is completely destroyed (removed).
        }//this bracket closes the else statement
    }//this bracket closes the defintion of the function


    public void buttonClick() {//this function is called when the reset button is clicked
        missed = 0;//this resets the count of cubes that the player has missed to 0
        _score = 0;//this resets the count of cubes that the player has hit to 0
    }//this closes the definition of the buttonClick function
}//this bracket ends the defition of the 'CubeMaker' class
