using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManagement : MonoBehaviour
{
    private static FMOD.Studio.EventInstance Music;
    public int maxHealth = 3;
    public int currentHealth;
    [SerializeField] private int musicLevel = 1;
    [SerializeField] private Transform liveContainer;

    private static HealthManagement _instance;
    public static HealthManagement Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    void Start()
    {
        currentHealth = maxHealth;
        Music = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music_Track_01");
        Music.start();
        Music.release();
        Music.setParameterByName("Heart_Level", currentHealth);
    }

    private void OnDestroy()
    {
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void OnChnageHealth()
    {
        currentHealth--;
        musicLevel++;
        if (currentHealth == 0)
        {
            // Granny death
            Animator animator = GameObject.FindGameObjectWithTag("Inhabitant").GetComponent<CharacterMovement>().animator;
            animator.SetTrigger("isDead");

            Invoke(nameof(GameOver), 3f);
        }

        Music.setParameterByName("Heart_Level", musicLevel);

        removeLive();
    }

    private void removeLive()
    {
        liveContainer.GetChild(0).gameObject.SetActive(false);
    }

    private void GameOver()
    {
        SceneManager.LoadScene("Game Over");
    }

}
