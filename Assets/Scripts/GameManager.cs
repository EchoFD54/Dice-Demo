using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour{
    void Update(){
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame){
            QuitGame();
        }
    }

    private void QuitGame(){
        Debug.Log("ESC pressed; exiting game");
        Application.Quit();
    }
}