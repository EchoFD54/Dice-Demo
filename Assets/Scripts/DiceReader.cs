using UnityEngine;
using System;   

public class DiceReader : MonoBehaviour{
    public event Action<int> OnDiceResult;
    private struct DiceFace{
        public Vector3 localDirection;
        public int value;

        public DiceFace(Vector3 direction, int value){
            this.localDirection = direction;
            this.value = value;
        }
    }
    
    private DiceFace[] diceFaces;

    void Awake(){
        diceFaces = new DiceFace[]{
            new DiceFace(Vector3.up, 2),
            new DiceFace(Vector3.down, 5),
            new DiceFace(Vector3.left, 3),
            new DiceFace(Vector3.right, 4),
            new DiceFace(Vector3.forward, 1),
            new DiceFace(Vector3.back, 6)
        };
    }

   
    void Update(){
        
    }

    public void ReadDiceFace(){
        Debug.Log("Dice has stopped :)");
        float highestDot = -2f;
        int rolledValue = 0;

        foreach(var face in diceFaces){
            Vector3 worldDirection = transform.TransformDirection(face.localDirection);
            float dot = Vector3.Dot(Vector3.up, worldDirection);
            if(dot > highestDot){
                highestDot = dot;
                rolledValue = face.value;
            }
        }

        if(highestDot > 0.9f){
            Debug.Log("Rolled result: " + rolledValue);
            OnDiceResult?.Invoke(rolledValue);
         } else{
            Debug.Log("Dice landed in a weird way, try again");
        }
    }
}
