using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _foundTextField;

    [SerializeField] private Gatherables _gameManagerGatherables;
    [SerializeField] private CanvasGroup _itemPanel;

    void Start()
    {
        SetStartText();
        _gameManagerGatherables.onRemainingUpdated += UpdateFoundText;
        _itemPanel.alpha = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _itemPanel.alpha = 1;
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _itemPanel.alpha = 0;
        }
    }

    private void SetStartText(){
       _foundTextField.text = "Remaining: " + _gameManagerGatherables.GetRemainingGatherables();
    }
    private void UpdateFoundText()
    {
        _foundTextField.text = "Remaining: " +  _gameManagerGatherables.GetRemainingGatherables();
    }

    
}
