using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherables : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gatherables;
    [SerializeField] private GameObject _endGameCanvas;
    [SerializeField] AudioClip foundItemSFX;
    private List<Gatherable> _foundGatherables;
    private bool _gameOver;
    public System.Action onGameOver;
    public System.Action onRemainingUpdated;
    
    private void Awake(){
        _foundGatherables = new List<Gatherable>();
        //GetRemainingGatherables();
    }
    void Start(){
        print($"num of found items: {_foundGatherables.Count}");
        _endGameCanvas.SetActive(false);
        Gatherable.onFound += FoundGatherable;
        onGameOver += GameOverCallback;
        _gameOver = false;
        SpawnGatherables();
    }
    
    private void SpawnGatherables(){
        foreach(GameObject obj in _gatherables){
            GameObject spawned = GameObject.Instantiate(obj);
            Gatherable current;
            if(spawned.TryGetComponent<Gatherable>(out current)){
                current.Init();
                Gatherable.LocationData spawnPoint = current.PickRandomSpawn();
                
                current.transform.position = spawnPoint.GetPosition();
                current.transform.eulerAngles = spawnPoint.GetRotation();
                //print($"Picked spawn for {current.name}");
                //print($"Position: {spawnPoint.GetPosition()}\nRotation:{spawnPoint.GetRotation()}");
            }else{
                print("Couldn't get component 'Gatherable'");
            }
        }
    }

    public int GetRemainingGatherables(){
        //print("Gatherables: " + _gatherables.Count + "\n\tFound: " + _foundGatherables.Count);
        return _gatherables.Count - _foundGatherables.Count;
    }
    public bool IsGameOver(){
        return _gameOver;
    }
    private void FoundGatherable(Gatherable _self){
        //print("remaining: " + GetRemainingGatherables());
        AudioManager.Instance.PlaySFX(foundItemSFX);
        _foundGatherables.Add(_self);
        onRemainingUpdated?.Invoke();
        if(GetRemainingGatherables() <= 0){
            onGameOver?.Invoke();
        }
    }
    private void GameOverCallback(){
        _endGameCanvas.SetActive(true);
        _gameOver = true;
    }
    private void OnDestroy(){
        Gatherable.onFound -= FoundGatherable;
        onGameOver -= GameOverCallback;
    }
}
