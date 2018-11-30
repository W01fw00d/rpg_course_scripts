using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonDestroyable : MonoBehaviour {

	void Awake () {
        DontDestroyOnLoad(this);
	}
}
