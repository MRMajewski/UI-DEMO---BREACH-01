using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource; 
    [SerializeField] private Button musicToggleButton;
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;
    [SerializeField] private List<SoundEntry> soundEntries; 

    private Dictionary<string, AudioClip> soundEffects = new Dictionary<string, AudioClip>(); 
    private bool isPlaying = true;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AddSoundsToDictionary(soundEntries);

        if (musicToggleButton != null)
        {
            musicToggleButton.onClick.AddListener(ToggleMusic);
        }

        UpdateButtonIcon();



        void AddSoundsToDictionary(List<SoundEntry> soundEntries)
        {
            foreach (var entry in soundEntries)
            {
                if (!soundEffects.ContainsKey(entry.name))
                {
                    soundEffects.Add(entry.name, entry.clip);
                }
                else
                {
                    Debug.LogWarning($"Duplikat nazwy dŸwiêku: {entry.name}");
                }
            }
        }
    }


    public void ToggleMusic()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {
            musicSource.Play();
        }
        else
        {
            musicSource.Pause();
        }

        UpdateButtonIcon();
    }

    public void PlaySound(string soundName)
    {
        if (soundEffects.TryGetValue(soundName, out AudioClip clip))
        {
            sfxSource.PlayOneShot(clip); 
        }
        else
        {
            Debug.LogWarning($"Nie znaleziono dŸwiêku: {soundName}");
        }
    }

    private void UpdateButtonIcon()
    {
        if (musicToggleButton == null) return;

        Image buttonImage = musicToggleButton.GetComponent<Image>();
        if (buttonImage != null)
        {
            buttonImage.sprite = isPlaying ? musicOnSprite : musicOffSprite;
        }
    }

    public void PlayPanelTransitionSFX()
    {
        PlaySound("TransitionSFX");
    }

    [System.Serializable]
    public class SoundEntry
    {
        public string name;
        public AudioClip clip;
    }
}
