using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtcMenu : MonoBehaviour {
	public Button first;
	public Button second;
	public Button third;
	public Button fourth;
	public Canvas atcm;
	public StartUp someVars;
	public SoundScript audioS;

	public bool firstpressed = false;
	public bool secondpressed = false;
	public bool thirdpressed = false;
	public bool fourthpressed = false;
	// Use this for initialization
	void Start () {
		first = first.GetComponent<Button> ();
		second = second.GetComponent<Button> ();
		third = third.GetComponent<Button> ();
		fourth = fourth.GetComponent<Button> ();
		atcm = atcm.GetComponent<Canvas> ();
		someVars = someVars.GetComponent<StartUp> ();
		audioS = audioS.GetComponent<SoundScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (someVars.freqFound == true) {
			allowATC ();
		}
		if (someVars.freqFound == true && firstpressed == false) {
			first.interactable = true;
		}
		if (someVars.reactorArmed == true&&secondpressed == false) {
			second.interactable = true;
		}
		if (someVars.landingLights == true&&thirdpressed == false) {
			third.interactable = true;
		}


	}
	public void allowATC()
	{
		if (atcm.enabled == false && Input.GetKeyDown (KeyCode.Backslash)) {
			atcm.enabled = true;
		} else if (atcm.enabled == true && Input.GetKeyDown (KeyCode.Backslash)) {
			atcm.enabled = false;
		}
	}
	public void checkInP()
	{
		firstpressed = true;
		first.interactable = false;
		atcm.enabled = false;
		audioS.reqCI ();
	}
	public void startP()
	{
		secondpressed = true;
		second.interactable = false;
		atcm.enabled = false;
		audioS.reqSC ();
	}
	public void taxiP()
	{
		thirdpressed = true;
		third.interactable = false;
		atcm.enabled = false;
		fourth.interactable = true;
		audioS.reqTC ();
	}
	public void takeP()
	{
		fourthpressed = true;
		fourth.interactable = false;
		atcm.enabled = false;
		audioS.reqTOC ();
	}
}
