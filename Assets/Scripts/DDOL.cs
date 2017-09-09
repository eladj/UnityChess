using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour {

	public LevelManager levelManager;

	public void Awake () {
		DontDestroyOnLoad (gameObject);
		levelManager = Object.FindObjectOfType<LevelManager>();
	}

	void Start(){
		levelManager.LoadLevel ("01_Main");
	}
}
