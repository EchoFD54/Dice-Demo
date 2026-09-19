using TMPro;
using UnityEngine;

public class DiceUI : MonoBehaviour{

    [SerializeField]
    private DiceController diceController;
    [SerializeField]
    private DiceReader diceReader;
    [SerializeField]
    private Transform diceTransform;
    [SerializeField] 
    private TMP_Text resultText;
    // Offset do that the text is above the dice 
    [SerializeField] 
    private Vector3 offset = new Vector3(0, 2f, 0);

    private void HideText(){
        resultText.gameObject.SetActive(false);
    }

    private void ShowResult(int result){
        resultText.text = result.ToString();
        resultText.gameObject.SetActive(true);
    }

    void OnEnable(){
        if(diceController != null){
            diceController.OnRollStarted += HideText;
        } else{
            Debug.LogWarning("diceController is not assigned on DiceUI");
        }

        if(diceReader != null){
            diceReader.OnDiceResult += ShowResult;
        } else{
            Debug.LogWarning("diceReader is not assigned on DiceUI");
        }
    }

    void OnDisable(){
        if(diceController != null){
            diceController.OnRollStarted -= HideText;
        }

        if(diceReader != null){
            diceReader.OnDiceResult -= ShowResult;
        }
    }

    void Start(){
            HideText();
    }

    void LateUpdate(){
        if(diceTransform != null){
            transform.position = diceTransform.position + offset;
            
            // this is to make sure the text is always facing the camera
            if (Camera.main != null) {
                transform.rotation = Quaternion.LookRotation(transform.position - Camera.main.transform.position);
            }
        } else{
            Debug.LogWarning("diceTransform is not assigned on DiceUI");
        }
    }
}
