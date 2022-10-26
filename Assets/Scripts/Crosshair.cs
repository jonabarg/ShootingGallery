using System.Collections;//dependency
using System.Collections.Generic;//dependency
using UnityEngine;//dependency

public class Crosshair : MonoBehaviour {//begins class definition
    [SerializeField]//set next variable to be definable from Unity inspector
    protected RectTransform crosshair;//this is an image defined from the Unity inspector

    protected void Update() {//this begins the definition of the Update function, also known as 'the game loop' and runs once each frame
        Vector2 mousePos = Input.mousePosition;//creates a variable to hold the mouses current position and assigns the mouses current position to it
        crosshair.position = mousePos;//set the position of the crosshair object to be at the current mouse position

        if (mousePos.x > 0 && mousePos.y > 0 && mousePos.x < Screen.width && mousePos.y < Screen.height) {//if the mouse cursor is within the game screen, the following code is executed
            Cursor.visible = false;//the cursor is made invisible
            crosshair.gameObject.SetActive(true);//the crosshair is made active (visible)
        }//this bracket closes the if statement
        else {//if the condition of the previous if statement is not true, the following code is executed
            Cursor.visible = true;//the cursor is made visible
            crosshair.gameObject.SetActive(false);//the crosshair is disabled (no longer visible)
        }//this bracket closes the else statement
    }//this bracket ends the definition of the Update function
}//this bracket ends the definition of the class 'Crosshair'
