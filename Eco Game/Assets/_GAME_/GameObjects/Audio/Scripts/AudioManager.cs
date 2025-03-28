using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----audio sources----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("----audio clips----")]
    public AudioClip SFX_AMBIANCE_wind;
    public AudioClip SFX_AMBIANCE_birds;
    public AudioClip SFX_AMBIANCE_dog;
    public AudioClip SFX_AMBIANCE_cat;
    public AudioClip SFX_MONSTER_HIT_normal;
    public AudioClip SFX_MONSTER_death;
    public AudioClip SFX_ATTACK_low;
    public AudioClip SFX_ATTACK_high;
    public AudioClip SFX_HEAL;
    public AudioClip SFX_HEAL_can_heal;
    public AudioClip SFX_HEAL_cannot_heal;
    public AudioClip SFX_PLAYER_power_up;
    public AudioClip SFX_PLAYER_HIT_normal;
    public AudioClip SFX_PLAYER_HIT_hard;
    public AudioClip SFX_UI_text;
    public AudioClip SFX_UI_hover;
    public AudioClip SFX_UI_select;
    public AudioClip SFX_UI_back;
    public AudioClip SFX_UI_start;
    public AudioClip SFX_UI_gameOver;
    public AudioClip SFX_WALK_grass;
    public AudioClip SFX_WALK_street;
    public AudioClip SFX_WALK_wood;

    public AudioClip OST_play;
    public AudioClip OST_menu;

    public AudioClip OST_currentOST;
    // OST playing and maybe game over.

    // private void start, starts with the music.

    private void Awake()
    {
        musicSource.clip = OST_currentOST;
        musicSource.Play();
    }

    public void playSFX (AudioClip sfx)
    {
        SFXSource.PlayOneShot(sfx);
    }

}
