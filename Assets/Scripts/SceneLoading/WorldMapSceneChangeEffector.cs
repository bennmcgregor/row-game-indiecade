using UnityEngine;

public class WorldMapSceneChangeEffector : MonoBehaviour
{
    [SerializeField] private string _sceneName;

    private WorldMapSceneLoader _sceneLoader;

    private void Start()
    {
        _sceneLoader = FindObjectOfType<WorldMapSceneLoader>();
    }

    protected void ActivateScene()
    {
        _sceneLoader.ActivateScene(_sceneName);
    }
}