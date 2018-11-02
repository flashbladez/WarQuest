using UnityEngine;
using System.Collections;

public class GameTime : MonoBehaviour {
	public enum TimeOfDay{
		Idle,
		Sunrise,
		Sunset
	}
	public Transform[] sun;				//array of our suns
	public float dayLengthInMinutes = 1; //length of day in game
	public float sunRise;				 //sunrise.
	public float sunSet;
	public float skyboxBlendModifier;	//How fast To blend Skybox.

	private float dayCycleInSeconds;
	private Sun[] sunScript;
	private const float SECOND = 1f;
	private const float MINUTE = 60 * SECOND;
	private const float HOUR = 60 * MINUTE;
	private const float DAY = 24 * HOUR;

	private const float DEGREES_PER_SECOND = 360 / DAY;
	private float degreeRotation;
	private float timeOfDay;

	private TimeOfDay tod;
	void Start () {
        sun[0] = GameObject.FindGameObjectWithTag("DLSun").transform;
        sun[1] = GameObject.FindGameObjectWithTag("DLMoon").transform;
        tod = TimeOfDay.Idle;
		dayCycleInSeconds = dayLengthInMinutes * MINUTE;
		sunScript = new Sun[sun.Length];
		RenderSettings.skybox.SetFloat ("_Blend",0);
		for (int cnt = 0; cnt < sun.Length; cnt++) {
			Sun temp = sun [cnt].GetComponent<Sun> ();
		
			if (temp == null) {
				Debug.LogWarning("Sun component not found adding it....");
				sun[cnt].gameObject.AddComponent<Sun>();
				temp = sun [cnt].GetComponent<Sun> ();
			}
			sunScript[cnt] = temp;
		}
		timeOfDay = 0;
		degreeRotation = DEGREES_PER_SECOND * DAY / (dayCycleInSeconds);
		sunRise *= dayCycleInSeconds;
		sunSet *= dayCycleInSeconds;
	}

	// Update is called once per frame
	void Update () {
	//	float _degreeRotation1 = _degreeRotation;
		//for (int cnt = 0; cnt<sun.Length; cnt++) {
			sun [0].Rotate (new Vector3 (degreeRotation, 0, 0) * Time.deltaTime);
			sun [1].Rotate (new Vector3 (-degreeRotation, 0, 0) * Time.deltaTime);
			timeOfDay += Time.deltaTime;
			//Debug.Log (timeOfDay);
		if (timeOfDay >= dayCycleInSeconds) { //reset the box
			timeOfDay -= dayCycleInSeconds;

		}
		if(timeOfDay > sunRise && timeOfDay < sunSet && RenderSettings.skybox.GetFloat ("_Blend") < 1){
			tod = GameTime.TimeOfDay.Sunrise;
			BlendSkyBox ();
		}else if(timeOfDay > sunSet && RenderSettings.skybox.GetFloat ("_Blend") > 0){
			tod = GameTime.TimeOfDay.Sunset;
			BlendSkyBox ();
		}else{
			tod = GameTime.TimeOfDay.Idle;
		}

	}

    //Todo make this seemlesly blend from night to day sky without jumping
	private void BlendSkyBox(){
		float temp = 0;
		switch(tod){
		case TimeOfDay.Sunrise:
			temp = (timeOfDay - sunRise) / dayCycleInSeconds * skyboxBlendModifier;
			break;
		case TimeOfDay.Sunset:
			temp = (timeOfDay - sunSet) / dayCycleInSeconds * skyboxBlendModifier;
			temp = 1 - (temp - 1);

			break;
		}
        print(temp);
		RenderSettings.skybox.SetFloat ("_Blend",temp);
		Debug.Log (temp);
	}
}
