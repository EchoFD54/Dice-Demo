using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceController : MonoBehaviour{
    [SerializeField] 
    private float minRollForce = 10f;
    [SerializeField]
    private float maxRollForce = 20f;
    [SerializeField]
    private float minRollTorque = 10f;
    [SerializeField]
    private float maxRollTorque = 20f;

    private Rigidbody rb;
    private float rollTimer = 0f;
    private DiceReader diceReader;
    public bool isRolling = false;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        diceReader = GetComponent<DiceReader>();
    }

    void FixedUpdate(){
        if(isRolling){
            rollTimer += Time.fixedDeltaTime;
            //wait here a couple seconds before checking if the dice has stopped rolling
            if(rollTimer > 3f){
                if(rb.linearVelocity.magnitude < 0.1f && rb.angularVelocity.magnitude < 0.1f){
                    isRolling = false;
                    if (diceReader != null){
                        diceReader.ReadDiceFace();
                    } else{
                        Debug.LogWarning("dicereader not found on the dice object");
                    }
                }
            }
        }
    }


    void Update(){
        if(Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isRolling){
            Debug.Log("space key was pressed and rolled the dice");
            Roll();
        }
    }

    void Roll(){
        if(!isRolling){
            rollTimer = 0f;
            isRolling = true;
            float rollForce = UnityEngine.Random.Range(minRollForce, maxRollForce);
            float rollTorque = UnityEngine.Random.Range(minRollTorque, maxRollTorque);
            Vector3 throwDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, UnityEngine.Random.Range(-1f, 1f)).normalized;
            rb.AddForce(throwDirection * rollForce, ForceMode.Impulse);
            rb.AddTorque(UnityEngine.Random.insideUnitSphere * rollTorque, ForceMode.Impulse);
        }
    }

  
}
