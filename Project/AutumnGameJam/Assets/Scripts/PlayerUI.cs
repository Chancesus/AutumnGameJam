using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _foundTextField;

    [SerializeField] private Gatherables _gameManagerGatherables;
    [SerializeField] private CanvasGroup _itemPanel;

    [SerializeField] private GameObject _itemsList;
    [SerializeField] private GameObject _itemNamePrefab;

    private List<TMP_Text> _remainingItems;
    void Start()
    {
        _remainingItems = new List<TMP_Text>();
        SetStartText();
        _gameManagerGatherables.onRemainingUpdated += UpdateFoundText;
        _itemPanel.alpha = 0;
        _itemPanel.interactable = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _itemPanel.alpha = 1;
            _itemPanel.interactable = true; 
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            _itemPanel.alpha = 0;
            _itemPanel.interactable= false;
        }
    }

    private void SetStartText(){
       _foundTextField.text = "Remaining: " + _gameManagerGatherables.GetRemainingGatherables();
    }
    private void UpdateFoundText()
    {
        _foundTextField.text = "Remaining: " +  _gameManagerGatherables.GetRemainingGatherables();
    }
    public void AddItemName(GameObject _gameObject){
        GameObject itemNameText = Instantiate(_itemNamePrefab, _itemsList.transform);
        TMP_Text text;
        string[] segments = _gameObject.name.Split("(", 2); //Remove "(Clone)" from name
        string name = segments[0];

        if(itemNameText.TryGetComponent<TMP_Text>(out text)){
            _remainingItems.Add(text);
            text.text = name;
        }
    }
    public void RemoveItemText(GameObject item){
        string[] segments = item.name.Split("(", 2); //Remove "(Clone)" from name
        string name = segments[0];
        TMP_Text toRemove = _remainingItems.Find(
            delegate(TMP_Text text){
                return text.text == name;
            }
        );
        Destroy(toRemove.gameObject);
    }

    
}
