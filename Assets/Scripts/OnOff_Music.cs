using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public AudioSource audioSource;

    private bool isPlaying = false;

    public Sprite playIcon;
    public Sprite pauseIcon;
    public Image buttonImage;

    public void ToggleMusic()
    {
        if (isPlaying)
        {
            audioSource.enabled = true;
            isPlaying = false;
            buttonImage.GetComponent<Image>().sprite = playIcon;
        }
        else
        {
            audioSource.enabled = false;
            isPlaying = true;
            buttonImage.GetComponent<Image>().sprite = pauseIcon;
        }
    }
}