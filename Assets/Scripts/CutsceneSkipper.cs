using UnityEngine;
using UnityEngine.Playables;

public class CutsceneSkipper : MonoBehaviour
{
    public float skipTo;
    private PlayableDirector cutscene;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cutscene = GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            cutscene.time = skipTo;
        }
    }

    public void GoNextLevel()
    {
        SceneLoader.LoadNextScene();
    }
}
