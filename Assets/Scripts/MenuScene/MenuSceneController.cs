using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip[] audioClips;

    public void StartGame()
    {
      UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }

    public void PlaySound(int soundIndex)
    {
      audioSource.PlayOneShot(audioClips[soundIndex]);
    }
}
