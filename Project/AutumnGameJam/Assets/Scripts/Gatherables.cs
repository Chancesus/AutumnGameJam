using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherables : MonoBehaviour
{
    [SerializeField] private List<GameObject> _gatherables;
    private List<Gatherable> _foundGatherables;
    
    private void Awake(){
        _foundGatherables = new List<Gatherable>();
        Gatherable.onFound += FoundGatherable;
        GetRemainingGatherables();
    }
    void Start(){
        SpawnGatherables();
    }
    
    private void SpawnGatherables(){
        foreach(GameObject obj in _gatherables){
            GameObject.Instantiate(obj);
            Gatherable current;
            if(obj.TryGetComponent<Gatherable>(out current)){
                current.PickRandomSpawn();
            }
        }
    }

    public int GetRemainingGatherables(){
        //print(_gatherables.Capacity);
        return _gatherables.Count - _foundGatherables.Count;
    }
    private void FoundGatherable(Gatherable _self){
        _foundGatherables.Add(_self);
    }
}
