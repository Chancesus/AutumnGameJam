using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{

    [SerializeField] private List<Transform> _spawnLocations;
    public static System.Action<Gatherable> onFound;
    public Transform PickRandomSpawn(){
        if(_spawnLocations == null || _spawnLocations.Count == 0){
            Debug.LogError("List is empty");
            return null;
        }
        
        int index = Mathf.FloorToInt(Random.Range(0, _spawnLocations.Count));
        return _spawnLocations[index];
    }
}
