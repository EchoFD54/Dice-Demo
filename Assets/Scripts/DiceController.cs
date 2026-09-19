using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiceController : MonoBehaviour{
    [SerializeField] 
    private float minRollForce = 15f;
    [SerializeField]
    private float maxRollForce = 30f;
    [SerializeField]
    private float minRollTorque = 15f;
    [SerializeField]
    private float maxRollTorque = 30f;

    private Rigidbody rb;
    private float rollTimer = 0f;
    private DiceReader diceReader;
    private float settleTimer = 0f;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public event Action OnDiceReset;
    public bool isRolling = false;
    public event Action OnRollStarted;

    void Awake(){
        rb = GetComponent<Rigidbody>();
        diceReader = GetComponent<DiceReader>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    void FixedUpdate(){
        if(isRolling){
            rollTimer += Time.fixedDeltaTime;
            //wait here a couple seconds before checking if the dice has stopped rolling
            if(rollTimer > 2f){
                if(rb.linearVelocity.magnitude < 0.05f && rb.angularVelocity.magnitude < 0.05f){
                    settleTimer += Time.fixedDeltaTime;
                    // if its staying still for a second then we can assume it has stopped rolling
                    if(settleTimer < 1f){
                        isRolling = false;
                        if (diceReader != null){
                            diceReader.ReadDiceFace();
                        } else{
                            Debug.LogWarning("dicereader not found on the dice object");
                        }
                    } else{
                        settleTimer = 0f;
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

        if(Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame){
            ResetDice();
        }
    }

    void Roll(){
        if(!isRolling){
            rollTimer = 0f;
            isRolling = true;
            OnRollStarted?.Invoke();
            float rollForce = UnityEngine.Random.Range(minRollForce, maxRollForce);
            float rollTorque = UnityEngine.Random.Range(minRollTorque, maxRollTorque);
            Vector3 throwDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), 1f, UnityEngine.Random.Range(-1f, 1f)).normalized;
            rb.AddForce(throwDirection * rollForce, ForceMode.Impulse);
            rb.AddTorque(UnityEngine.Random.insideUnitSphere * rollTorque, ForceMode.Impulse);
        }
    }


    void ResetDice(){
        isRolling = false;
        rollTimer = 0f;
        settleTimer = 0f;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
        OnDiceReset?.Invoke();

        Debug.Log("Dice has been reset to the original position and rotation");
    }

  
}
