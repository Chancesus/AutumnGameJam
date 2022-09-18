using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _foundTextField;

    [SerializeField] private Gatherables _gameManagerGatherables;

    void Start()
    {
        SetStartText();
        Gatherable.onFound += UpdateFoundText;
    }
    private void SetStartText(){
       _foundTextField.text = "Remaining: " + _gameManagerGatherables.GetRemainingGatherables();
    }
    private void UpdateFoundText(Gatherable _gatherable)
    {
        _foundTextField.text = "Remaining: " +  _gameManagerGatherables.GetRemainingGatherables();
    }

    
}
