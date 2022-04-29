using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip MenuMusic;
    public AudioClip GameMusic;
    public AudioMixer audioMixer;

    private string _volumeParameter = "volume";
    private bool _gameMusic = false;

    private void SetVolume(float value)
    {
        audioMixer.SetFloat(_volumeParameter, Mathf.Log10(value) * 30.0f);
    }

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        SetVolume(PlayerPrefs.GetFloat(_volumeParameter, 0.5f));
    }

    private void ChangeMusic(AudioClip clip)
    {
        audioMixer.GetFloat(_volumeParameter, out float tempVolume);
        if (tempVolume <= -80.0f)
        {
            audioSource.clip = clip;
            _gameMusic = !_gameMusic;
            SetVolume(PlayerPrefs.GetFloat(_volumeParameter, 0.5f));
            audioSource.Play();
        }
        else
            audioMixer.SetFloat(_volumeParameter, tempVolume - Time.fixedDeltaTime * PlayerPrefs.GetFloat(_volumeParameter, 0.5f) * 80.0f);
    }

    private void FixedUpdate()
    {
        if (!_gameMusic && SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1)
            ChangeMusic(GameMusic);
        else if (_gameMusic && SceneManager.GetActiveScene().buildIndex != SceneManager.sceneCountInBuildSettings - 1)
            ChangeMusic(MenuMusic);
    }
}
