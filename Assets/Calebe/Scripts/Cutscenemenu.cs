using UnityEngine;
using UnityEngine.Playables;

public class Cutscenemenu : MonoBehaviour
{
    public PlayableDirector director;

    public void Play()
    {
        director.Play();
    }
}