﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

	[Range (1, 10)]
	public float mulletMass = 1;

	[Range (0, 1)]
	public float mulletPositionTreshold = 0.3f;

	[Range (1, 10)]
	public float accelerationForce = 1f;
}
