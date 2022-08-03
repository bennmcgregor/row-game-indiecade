using UnityEngine;

public class WorldMapSceneChangeRegion : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private string _sceneName;

    private WorldMapSceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<WorldMapSceneLoader>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == _player.name)
        {
            _sceneLoader.LoadScene(_sceneName);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == _player.name)
        {
            _sceneLoader.UnloadScene(_sceneName);
        }
    }
}