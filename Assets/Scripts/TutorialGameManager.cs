using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialGameManager : MonoBehaviour
{
    /*
    [Header("Events")]
    public UnityEvent OnDeath; //evento que se llama cuando la vida del jugador baja a 0 
    public UnityEvent OnPrewin; //evento que se llama cuando el jugador activa los 4 espejos
    public UnityEvent OnWin; // evento que se llama cuando el jugador consigue la llave
    */

    public GameObject player;
    public PlayerController pc;
    public GameObject[] enemies;
    private Transform[] originalEnemyPosition;
    public Vector2 respawnPoint = new Vector2((float)0, (float)4.5);
    public CanvasGroup UIingame, fadeBlack, fadeWhite, tutorialTexts;
    public SpriteRenderer text;

    public Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = new Vector2 (200, 200);

    private float counterD, counterW;
    private bool won, isWinnable;

    private void Start()
    {
        counterD = 0f;
        counterW = 0f;
        won = false;
        isWinnable = false;
        text.color = new Color(1f, 1f, 1f, 0f);
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        originalEnemyPosition = new Transform[enemies.Length];
        GetEnemyPosition();
    }

    void Update()
    {
        if (Input.GetButtonDown("Use") && isWinnable)
        {
            won = true;
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

    void Win()
    {
        UIingame.alpha -= Time.deltaTime * 1;
        tutorialTexts.alpha -= Time.deltaTime * 1;
        fadeWhite.alpha += Time.deltaTime * 0.5f;
        // you win
        // canvas de victoria

        if (counterW > 5)
        {
            SceneManager.LoadScene("MenuCarga2");
        }
    }

    void IsDead()
    {
        UIingame.alpha -= Time.deltaTime;
        tutorialTexts.alpha -= Time.deltaTime * 1;
        fadeBlack.alpha += Time.deltaTime * 0.5f;

        // you died
        // canvas de derrota

        if (counterD > 5)
        {
            for (int x = 0; x < enemies.Length; x++)
            {
                enemies[x].SetActive(true);
                enemies[x].transform.position = originalEnemyPosition[x].position;
            }

            pc.Respawn(respawnPoint);
            UIingame.alpha = 1;
            tutorialTexts.alpha = 1;
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

    public void OnTriggerEnter2D(Collider2D collision)
    {
        text.color = new Color(1f, 1f, 1f, 1f);
        isWinnable = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        text.color = new Color(1f, 1f, 1f, 0f);
        isWinnable = false;
    }
}
