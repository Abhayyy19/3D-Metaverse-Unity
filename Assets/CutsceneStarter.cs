using UnityEngine;
using UnityEngine.Playables;

public class CutsceneStarter : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;

    void Start()
    {
        Invoke("StartCutscene", 10f);
    }

    void StartCutscene()
    {
        // Check if the cutsceneDirector is assigned
        if (cutsceneDirector != null)
        {
            cutsceneDirector.Play(); // Start the cutscene
        }
        else
        {
            Debug.LogWarning("Cutscene Director is not assigned!");
        }
    }
}