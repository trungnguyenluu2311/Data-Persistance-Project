using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIHandler : MonoBehaviour
{
	public static MenuUIHandler Instance;
	public TMP_InputField nameInputField;
	public TextMeshProUGUI bestScoreText;

	public string playerNameInput;
	public int bestScore;
	public string bestPlayerName;

	private void Awake()
	{
		if (Instance != null)
		{
			Destroy(gameObject);
			return;
		}

		Instance = this;
		DontDestroyOnLoad(gameObject);
		LoadData();
	}


	// Start is called before the first frame update
	void Start()
	{
		bestScoreText.text = "Best Score : " + bestPlayerName + " : " + bestScore;
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void StartScene()
	{
		playerNameInput = nameInputField.text;
		SceneManager.LoadScene(1);
		this.gameObject.SetActive(false);
	}

	public void Quit()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode(); //exit playmode in Unity Editor
#else
		Application.Quit(); // original code to quit Unity player
#endif
	}

	// Create new class to store data
	[System.Serializable]
	class SavedData
	{
		public int bestScore;
		public string bestPlayerName;
	}

	public void SaveData()
	{
		SavedData data = new SavedData(); // create new instance of SavedData class

		// Save data to SavedData class variables
		data.bestPlayerName = this.bestPlayerName;
		data.bestScore = this.bestScore;

		string json = JsonUtility.ToJson(data); // convert data to json
		File.WriteAllText(Application.persistentDataPath + "/savefile.json", json); // write to savefile.json
	}

	public void LoadData()
	{
		string path = Application.persistentDataPath + "/savefile.json";
		if (File.Exists(path))
		{
			string json = File.ReadAllText(path);
			SavedData data = JsonUtility.FromJson<SavedData>(json); // convert data from json to SavedData class instance

			// Load to MenuUIHandler class variables
			this.bestScore = data.bestScore;
			this.bestPlayerName = data.bestPlayerName;
		}
	}
}
