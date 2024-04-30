using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    //Es una función que define los diferentes estados del juego, ejemplo pausa
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    //Esta variable almacena el estado del juego en el momento
    public GameState currentState;
    public GameState previousState; //El estado antes del actual, este es para cuestiones de funcionalidad

    [Header("Screens")]
    public GameObject pauseScreen;
    public GameObject resultsScreen;

    //Las stats que se van a mostrar

    //Las stats que estan en el momento
    [Header("Current Stat Displays")]
    public Text currentHealthDisplay;
    public Text currentrecoveryDisplay;
    public Text currentMoveSpeedDisplay;
    public Text currentMightDisplay;
    public Text currentProjectileSpeedDisplay;
    public Text currentMagnetDisplay;

    [Header("Results Screen Display")]
    public Image chosenCharacterImage;
    public Text chosenCharacterName;

    public bool isGameOver = false;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("EXTRA" + this + "DELETED");
            Destroy(gameObject);
        }
        DisableScreen();
    }
    void Update()
    {
        //Es para cambiar la variable en cada de los métodos del GameState
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                break;
            case GameState.Paused:
                CheckForPauseAndResume();
                break;
            case GameState.GameOver:
                if(!isGameOver)
                {
                    isGameOver=true;
                    Time.timeScale = 0f;
                    Debug.Log("El juego terminó");
                    DisplayResults();
                }
                break;
            default:
            Debug.LogWarning("El estado del juego no se encuentra o no existe");
                break;
        }        
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if(currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f; //Para pausar el juego
            pauseScreen.SetActive(true);
            Debug.Log("El juego ha sido pausado");
        }
    }

    public void ResumeGame()
    {
        if(currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f; //Para resumir el tiempo del juego
            pauseScreen.SetActive(false);
            Debug.Log("El juego ha sido continuado");
        }
    }

    void CheckForPauseAndResume()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultsScreen.SetActive(false);
    }

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        resultsScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.name;
    }
}
