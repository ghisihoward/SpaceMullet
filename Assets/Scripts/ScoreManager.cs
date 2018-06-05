using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	public int HighScoreSize = 6;
	public List<Score> HighScores = new List<Score>();
	public List<string> HighScoresList = new List<string>();
	public string lastName = "";
	string json;

	void Start () {
		GetHighScore ();
	}

	void sortScore (string type) {
		if (type == "points") {
			HighScores.Sort ((p1, p2) => (p1.points == p2.points ? p2.points + p2.age : p2.points).CompareTo (p1.points == p2.points ? p1.points + p1.age : p1.points));
		} 
		else if (type == "age") {
			HighScores.Sort ((p1, p2) => (p1.age).CompareTo (p2.age));
		}
	}

	void SetHighScore () {
		sortScore ("points");
		HighScoresList.Clear ();

		while (HighScores.Count > HighScoreSize) {
			HighScores.RemoveAt (HighScores.Count - 1);
		}

		for (int i = 0; i < HighScores.Count; i++) {
			HighScores [i].age = (HighScores [i].age + 1);
		}

		sortScore ("age");

		for (int i = 0; i < HighScores.Count; i++) {
			if (i == 0) {
				HighScores [i].age = 1;
			} else {
				HighScores [i].age = HighScores [i - 1].age + 1;
			}
		}

		sortScore ("points");

		for (int i = 0; i < HighScores.Count; i++) {
			HighScoresList.Add (JsonUtility.ToJson (HighScores [i]));
		}

		json = JsonUtility.ToJson (this);
		PlayerPrefs.SetString ("Score", json);
		PlayerPrefs.Save ();
	}

	void GetHighScore () {
		json = PlayerPrefs.GetString ("Score", "");
		JsonUtility.FromJsonOverwrite (json, this);

		HighScores.Clear ();

		for (int i = 0; i < HighScoresList.Count; i++) {
			HighScores.Add(new Score ());
			JsonUtility.FromJsonOverwrite (HighScoresList[i], HighScores[i]);
		}
	}

	public void AddScore (string newName, float newPoint) {
		if ((HighScores.Count > HighScoreSize) ? (newPoint > HighScores [HighScores.Count - 1].points) : true) {
			Score newScoreObject = new Score ();
			newScoreObject.name = newName;
			lastName = newName;
			newScoreObject.points = newPoint;
			newScoreObject.age = 0;
			HighScores.Add (newScoreObject);
			SetHighScore ();
		}
	}

	public Score GetScoreAt (int pos) {
		try {
			return HighScores [pos - 1];
		} catch (System.ArgumentOutOfRangeException) {
			Score mockJoe = new Score ();
			mockJoe.age = 0;
			mockJoe.points = 0;
			mockJoe.name = "";

			return mockJoe;
		}
	}

	public float GetPointAt (int pos) {
		return HighScores [pos - 1].points;
	}

	public bool isHighScore (float newScore) {
		float scoreMin = ((HighScores.Count < HighScoreSize) ? 0 : HighScores [HighScores.Count - 1].points);
		return (newScore > scoreMin);
	}
}

public class Score {
	public int age;
	public float points;
	public string name;
}