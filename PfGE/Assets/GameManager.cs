using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	
	public enum SceneName {
		Menu,
		Game,
		Win
	}

	public List<string> scenes;

	public GameObject UI;
	public GameObject pauseUI;
	public PlayerController playerController;
	public GameObject playerPawn;

	public GameObject mainCamera;

	public static GameManager instance;
	
	public enum WeaponType { 
		None,
		M4A1,
		RPG
	};

	public List<GameObject> powerUps;

	public List<GameObject> weaponPrefabs;

	public List<Vector3> attachmentPointOffsets;

	public bool gameStarted = false;
	public bool paused = false;

	private bool gameUIsetUp = false;
	private bool menuUIsetUp = true;
	private bool winUIsetUp = false;

	public bool win = false;

	//on awake make sure the manager cannot be destroyed on scene loads
	void Awake() {
		if (instance == null) {
			instance = this;
			DontDestroyOnLoad(gameObject);
		} else {
			Destroy(gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameStarted) {
        	if (!UI) { UI = GameObject.Find("HUDCanvas"); }
        	if (!pauseUI) { pauseUI = GameObject.Find("PauseMenu"); }
        	if (!playerController && GameObject.FindWithTag("Player")) { playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); }
        	if (!playerPawn) { playerPawn = GameObject.Find("playerPawn"); }
			if (!mainCamera) { mainCamera = GameObject.Find("Main Camera"); }

        	if (Input.GetKeyDown(KeyCode.P)) {
        		TogglePause();
        	}

        	if (pauseUI && !gameUIsetUp) {
        		pauseUI.transform.Find("Resume").GetComponent<Button>().onClick.AddListener(TogglePause);
        		pauseUI.transform.Find("Return").GetComponent<Button>().onClick.AddListener(delegate {MoveScene("MainMenu"); });
        		pauseUI.transform.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseGame);
        		gameUIsetUp = true;
        	}

        	if (paused) {
        		if (pauseUI)
        			pauseUI.SetActive(true);
        	} else {
        		if (pauseUI)
        			pauseUI.SetActive(false);
        	}
        } else if (win) {
        	Cursor.visible = true;
        	if (!winUIsetUp) {
	        	if (GameObject.Find("Return")) 
	        		GameObject.Find("Return").GetComponent<Button>().onClick.AddListener(delegate {MoveScene("MainMenu"); });
	        	else return;
	        	if (GameObject.Find("Exit")) 
	        		GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseGame);
	        	else return;
	        	winUIsetUp = true;
	        }
        } else {
        	Cursor.visible = true;
        	if (!menuUIsetUp) {
	        	if (GameObject.Find("Start")) 
	        		GameObject.Find("Start").GetComponent<Button>().onClick.AddListener(delegate {MoveScene("SampleScene"); });
	        	else return;
	        	if (GameObject.Find("Exit"))
	        		GameObject.Find("Exit").GetComponent<Button>().onClick.AddListener(CloseGame);
	        	else return;
	        	menuUIsetUp = true;
	        }
        }
    }

    public void Win() {
    	win = true;
    	gameStarted = false;
    	MoveScene("WinScene");
    }

    public void TogglePause() {
    	paused = !paused;
    }

    public void CloseGame() {
    	Application.Quit();
    }

    public void MoveScene(string scene) {
    	SceneManager.LoadScene(scene);

    	paused = false;
		menuUIsetUp = false;
		winUIsetUp = false;
		gameUIsetUp = false;
		gameStarted = false;

    	if (scene == scenes[(int) SceneName.Game]) {
    		win = false;
    		gameStarted = true;
    		if (!UI) { UI = GameObject.Find("HUDCanvas"); }
        	if (!pauseUI) { pauseUI = GameObject.Find("PauseMenu"); }
        	if (!playerController && GameObject.FindWithTag("Player")) { playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>(); }
        	if (!playerPawn) { playerPawn = GameObject.Find("playerPawn"); }
			if (!mainCamera) { mainCamera = GameObject.Find("Main Camera"); }
    	} 
    }
}
