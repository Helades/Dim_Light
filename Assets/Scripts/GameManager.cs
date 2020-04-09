using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    /*
    [Header("Events")]
    public UnityEvent OnDeath; //evento que se llama cuando la vida del jugador baja a 0 
    public UnityEvent OnPrewin; //evento que se llama cuando el jugador activa los 4 espejos
    public UnityEvent OnWin; // evento que se llama cuando el jugador consigue la llave
    */

    public GameObject player, ra1, ra2, ra3, ra4, win;
    public PlayerController pc;
    public Light spotlight;
    public GameObject[] enemies, lightPlatforms;
    private Transform[] originalEnemyPosition, originalPlatformPosition;
    public Vector2 respawnPoint = new Vector2((float)30, (float)172.3);
    public CanvasGroup UIingame, fadeBlack, fadeWhite;
 
    private float counterD, counterW;
    private bool winSpawned;
    public bool won;

    private void Start()
    {
        counterD = 0f;
        counterW = 0f;
        win.SetActive(false);
        spotlight.enabled = false;
        winSpawned = false;

        originalEnemyPosition = new Transform[enemies.Length];
        originalPlatformPosition = new Transform[lightPlatforms.Length];

        GetEnemyPosition();
        GetLightPlatforms();
    }

    void Update()
    {
        if (ra1.GetComponent<RuneAltar>().isOn && ra2.GetComponent<RuneAltar>().isOn && ra3.GetComponent<RuneAltar>().isOn && ra4.GetComponent<RuneAltar>().isOn && !winSpawned)
         {
                PreWin();
                winSpawned = true;
        }

        if (player.GetComponent<PlayerController>().isDead)
        {
            counterD += Time.deltaTime;
            IsDead();
        }

        if (won)
        {
            counterW += Time.deltaTime;
            Win();
        }
    }

    void PreWin()
    {
        win.SetActive(true);
        spotlight.enabled = true;
    }

    void Win()
    {
        UIingame.alpha -= Time.deltaTime * 1;
        fadeWhite.alpha += Time.deltaTime * 0.5f;
        // you win
        // animación de fundido a negro
        // canvas de victoria

        if (counterW > 5)
        {
            SceneManager.LoadScene("MenuInicio");
        }
    }

    void IsDead()
    {
        UIingame.alpha -= Time.deltaTime;
        fadeBlack.alpha += Time.deltaTime * 0.5f;

        // you died
        // animación de fundido a negro
        // canvas de derrota

        if (counterD > 5)
        {
            for (int x = 0; x < enemies.Length; x++)
            {
                enemies[x].SetActive (true);
                enemies[x].transform.position = originalEnemyPosition[x].position;
            }
            for (int x = 0; x < lightPlatforms.Length; x++)
            {
                lightPlatforms[x].transform.position = originalPlatformPosition[x].position;
                lightPlatforms[x].GetComponent<MobilePlatform>().active = false;
            }
            pc.Respawn(respawnPoint);
            UIingame.alpha = 1;
            fadeBlack.alpha = 0;
            counterD = 0;
        }
    }

    void GetEnemyPosition()
    {
        for (int x = 0; x < enemies.Length; x++)
        {
            originalEnemyPosition[x] = enemies[x].transform;
        }
    }

    void GetLightPlatforms()
    {
        for (int x = 0; x < lightPlatforms.Length; x++)
        {
            originalPlatformPosition[x] = lightPlatforms[x].transform;
        }
    }
}

