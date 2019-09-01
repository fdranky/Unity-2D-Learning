using UnityEngine;

public class Level : MonoBehaviour
{
    // parameters
    [SerializeField] int breakableBlocks;

    // cached reference
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks() => breakableBlocks++;

    public void BreakableBlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }
}
