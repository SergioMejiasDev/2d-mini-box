using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

/// <summary>
/// Clase que se encarga de guardar y cargar las puntuaciones y las opciones en un archivo binario.
/// </summary>
public class SaveManager : MonoBehaviour
{
    public static SaveManager saveManager;

    /// <summary>
    /// Idioma activo.
    /// </summary>
    [Header("Options")]
    public string activeLanguage = "EN";
    /// <summary>
    /// Falso si es la primera vez que se inicia el juego.
    /// </summary>
    public bool firstTimeLanguage = false;
    /// <summary>
    /// Volumen del juego.
    /// </summary>
    public int gameVolume = 100;
    /// <summary>
    /// Pantalla completa activada.
    /// </summary>
    public bool fullscreen = false;

    /// <summary>
    /// Puntuación del juego 01.
    /// </summary>
    [Header("Scores")]
    public int score1 = 0;
    /// <summary>
    /// Puntuación del juego 02.
    /// </summary>
    public int[] score2 = new int[2] { 0, 0 };
    /// <summary>
    /// Puntuación del juego 03.
    /// </summary>
    public int score3 = 0;
    /// <summary>
    /// Puntuación del juego 04.
    /// </summary>
    public int score4 = 0;
    /// <summary>
    /// Puntuación del juego 05.
    /// </summary>
    public int score5 = 0;
    /// <summary>
    /// Puntuación del juego 06.
    /// </summary>
    public int score6 = 0;
    /// <summary>
    /// Puntuación del juego 07.
    /// </summary>
    public int score7 = 0;
    /// <summary>
    /// Puntuación del juego 08.
    /// </summary>
    public int score8 = 0;
    /// <summary>
    /// Puntuación del juego 09.
    /// </summary>
    public int score9 = 0;
    /// <summary>
    /// Puntuación del juego 10.
    /// </summary>
    public int score10 = 0;
    /// <summary>
    /// Puntuación del juego 11.
    /// </summary>
    public int[] score11 = new int[2] { 0, 0 };
    /// <summary>
    /// Puntuación del juego 12.
    /// </summary>
    public int score12 = 0;

    private void Awake()
    {
        // El objeto que contendrá la clase debe instanciarse una única vez, y se mantendrá en todas las escenas.
        // Si se generara un segundo objeto, se destruirá antes de activar sus funciones.

        GameObject[] objs = GameObject.FindGameObjectsWithTag("Menu/SaveManager");

        if (objs.Length > 1)
        {
            Destroy(gameObject);
        }

        else
        {
            saveManager = this;

            DontDestroyOnLoad(gameObject);

            LoadOptions();
            LoadScores();
        }
    }

    /// <summary>
    /// Función encargada de cargar los valores de las opciones.
    /// </summary>
    public void LoadOptions()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            activeLanguage = PlayerPrefs.GetString("ActiveLanguage", "EN");
            int _firstTimeLanguage = PlayerPrefs.GetInt("FirstTimeLanguage", 0);
            gameVolume = PlayerPrefs.GetInt("GameVolume", 100);

            firstTimeLanguage = _firstTimeLanguage == 1 ? true : false;

            return;
        }

        OptionsData data = new OptionsData();

        string path = Application.persistentDataPath + "/Options.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as OptionsData;
            stream.Close();

            activeLanguage = data.activeLanguage;
            firstTimeLanguage = data.firstTimeLanguage;
            gameVolume = data.gameVolume;
            fullscreen = data.fullscreen;
        }
    }

    /// <summary>
    /// Función encargada de guardar los valores de las opciones.
    /// </summary>
    public void SaveOptions()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            int _firstTimeLanguage = firstTimeLanguage == true ? 1 : 0;

            PlayerPrefs.SetString("ActiveLanguage", activeLanguage);
            PlayerPrefs.SetInt("FirstTimeLanguage", _firstTimeLanguage);
            PlayerPrefs.SetInt("GameVolume", gameVolume);

            return;
        }

        OptionsData data = new OptionsData
        {
            activeLanguage = activeLanguage,
            firstTimeLanguage = firstTimeLanguage,
            gameVolume = gameVolume,
            fullscreen = fullscreen
        };

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Options.sav";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, data);

        fileStream.Close();
    }

    /// <summary>
    /// Función encargada de cargar las puntuaciones.
    /// </summary>
    public void LoadScores()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            score1 = PlayerPrefs.GetInt("Score1");
            score2[0] = PlayerPrefs.GetInt("Score2-0");
            score2[1] = PlayerPrefs.GetInt("Score2-1");
            score3 = PlayerPrefs.GetInt("Score3");
            score4 = PlayerPrefs.GetInt("Score4");
            score5 = PlayerPrefs.GetInt("Score5");
            score6 = PlayerPrefs.GetInt("Score6");
            score7 = PlayerPrefs.GetInt("Score7");
            score8 = PlayerPrefs.GetInt("Score8");
            score9 = PlayerPrefs.GetInt("Score9");
            score10 = PlayerPrefs.GetInt("Score10");
            score11[0] = PlayerPrefs.GetInt("Score11-0");
            score11[1] = PlayerPrefs.GetInt("Score11-1");
            score12 = PlayerPrefs.GetInt("Score12");

            return;
        }

        ScoreData data = new ScoreData();
        
        string path = Application.persistentDataPath + "/Scores.sav";

        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();

            score1 = data.score1;
            score2 = data.score2;
            score3 = data.score3;
            score4 = data.score4;
            score5 = data.score5;
            score6 = data.score6;
            score7 = data.score7;
            score8 = data.score8;
            score9 = data.score9;
            score10 = data.score10;
            score11 = data.score11;
            score12 = data.score12;
        }
    }

    /// <summary>
    /// Función encargada de guardar las puntuaciones.
    /// </summary>
    public void SaveScores()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            PlayerPrefs.GetInt("Score1", score1);
            PlayerPrefs.GetInt("Score2-0");
            PlayerPrefs.GetInt("Score2-1");
            PlayerPrefs.GetInt("Score3", score3);
            PlayerPrefs.GetInt("Score4", score4);
            PlayerPrefs.GetInt("Score5", score5);
            PlayerPrefs.GetInt("Score6", score6);
            PlayerPrefs.GetInt("Score7", score7);
            PlayerPrefs.GetInt("Score8", score8);
            PlayerPrefs.GetInt("Score9", score9);
            PlayerPrefs.GetInt("Score10", score10);
            PlayerPrefs.GetInt("Score11-0");
            PlayerPrefs.GetInt("Score11-1");
            PlayerPrefs.GetInt("Score12", score12);

            return;
        }

        ScoreData data = new ScoreData
        {
            score1 = score1,
            score2 = score2,
            score3 = score3,
            score4 = score4,
            score5 = score5,
            score6 = score6,
            score7 = score7,
            score8 = score8,
            score9 = score9,
            score10 = score10,
            score11 = score11,
            score12 = score12
        };

        BinaryFormatter formatter = new BinaryFormatter();

        string path = Application.persistentDataPath + "/Scores.sav";

        FileStream fileStream = new FileStream(path, FileMode.Create);

        formatter.Serialize(fileStream, data);

        fileStream.Close();
    }

    /// <summary>
    /// Función encargada de borrar todas las puntuaciones.
    /// </summary>
    public void DeleteScores()
    {
        score1 = 0;
        score2 = new int[] { 0, 0 };
        score3 = 0;
        score4 = 0;
        score5 = 0;
        score6 = 0;
        score7 = 0;
        score8 = 0;
        score9 = 0;
        score10 = 0;
        score11 = new int[] { 0, 0 };
        score12 = 0;

        SaveScores();
    }
}