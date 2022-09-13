using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatherable : MonoBehaviour
{
    [System.Serializable]
    public class LocationData{
        [SerializeField]private Vector3 _position;
        [SerializeField]private Vector3 _rotation;
        public LocationData(Vector3 pos, Vector3 rot){
            _position = pos;
            _rotation = rot;
        }
        public LocationData(){
            _position = Vector3.zero;
            _rotation = Vector3.zero;
        }
    }

    [SerializeField] private List<LocationData> _spawnLocations;
    public static System.Action<Gatherable> onFound;

    void Start(){
        onFound += OnFoundCallback;
    }
    public LocationData PickRandomSpawn(){
        if(_spawnLocations == null || _spawnLocations.Count == 0){
            Debug.LogError("List is empty");
            return null;
        }
        
        int index = Mathf.FloorToInt(Random.Range(0, _spawnLocations.Count));
        return _spawnLocations[index];
    }

    public void PlayerFoundObject(){
        onFound?.Invoke(this);
    }

    private void OnFoundCallback(Gatherable _self){
        Destroy(_self.gameObject);
    }
    void OnDestroy(){
        onFound -= OnFoundCallback;
    }
}
