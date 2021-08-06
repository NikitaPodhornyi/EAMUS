using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartUp : MonoBehaviour {
	//If it looks stupid, but works, it aint stupid
#region Declaration
	public bool battery = false;
	public bool power = false;
	public bool preheat = false;
	public float tempEng = 0.0f;
	public bool exLights = false;
	public bool intLights = false;
	public bool canopyClosed = false;
	public bool sealShip = false;
	public bool commsModOn = false; 
	public bool freqFound = false;
	public bool fuelPumpFirst = false; 
	public bool fuelPumpSecond = false;
	public bool fuelPumpThird = false;
	public bool gearUp = false;
	public static bool parkingBreak = true; 
	public bool reactorArmed = false;
	public bool reactorOn = false;
	public bool reactorFuelCell = false;
	public bool engineLock = true;
	public bool engineStart = false;
	public bool primaryDisplay = false;
	public bool secondaryDisplay = false;
	public bool headsUpDisplay = false; 
	public bool probeHeat = false;
	public static bool thrusters = false; 
	public bool hydraulicPumpOne = false; 
	public bool hydraulicPumpSecond = false;
	public bool landingLights = false; 
	public bool spaceShipOn = false;
	public bool inertiaDumpener = false;
	public AudioClip EngineStart;
	public AudioClip preheatStart;
	public AudioClip GearUp;
	public AudioClip FuelPump;
	public AudioSource peStart;
	public AudioSource surround;
	public Light l_intLights;
	public Light l_extLigtsR;
	public Light l_extLigtsG;
	public Light l_extLigtsB;
	public Light l_lanLightsL;
	public Light l_lanLightsR;
	public Text next;
	public Text temp;
	public Text tempPre;
	public AtcMenu atcBool;
	public ParticleSystem engine1;
	public ParticleSystem engine2;

#endregion

	void Start () {
		atcBool = atcBool.GetComponent<AtcMenu> ();
		next = next.GetComponent<Text> ();
		temp = temp.GetComponent<Text> ();
		tempPre = tempPre.GetComponent<Text> ();
		engine1 = engine1.GetComponent<ParticleSystem> ();
		engine2 = engine2.GetComponent<ParticleSystem> ();
		engine1.enableEmission = false;
		engine2.enableEmission = false;
		tempPre.enabled = false;
		temp.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
		SoundScript audioScript = GetComponent<SoundScript>();
		Animator anim = GetComponent<Animator> ();
		//Light l_intLights = GetComponent<Light> ();
		
		if (battery==false&&Input.GetKeyDown (KeyCode.B)) //battery ON
		{
			next.text = "P";
			battery = true;
			audioScript.switch1 ();
		}
		else if (battery == true && Input.GetKeyDown (KeyCode.B)) {
			battery = false;
			audioScript.switch1 ();
		}
		if (power == false && battery == true && Input.GetKeyDown (KeyCode.P)) { //Power ON
		    next.text = "Num0";
			power = true;
			audioScript.switch3 ();
			audioScript.v_biBetty ();
		} else if (power == true && Input.GetKeyDown (KeyCode.P)) {
			power = false;
			audioScript.switch3 ();
		}
		if (preheat==false&&power == true && Input.GetKeyDown (KeyCode.Keypad0)) { //Engine preheat ON
			next.text = "1";
			preheat = true;//sound of clicking 
			audioScript.preheatWorking ();
			peStart.clip = preheatStart;
			peStart.Play ();
		} 
		else if (preheat == true && Input.GetKeyDown (KeyCode.Keypad0)) 
		{
			preheat = false;
			audioScript.preheatStop ();
		}
		if (preheat == true && power == true) { //Temperature Rise
			enginePreheat ();
		} else if (preheat == false) {
			tempPre.enabled = false;
			temp.enabled = false;
		}
		if (exLights == false&&power == true && Input.GetKeyDown (KeyCode.Alpha1)) //External Lights ON
		{
			next.text = "2";
			audioScript.switch2 ();
			exLights = true;
			l_extLigtsR.enabled = true;
			l_extLigtsG.enabled = true;
			l_extLigtsB.enabled = true;
		}else if (exLights == true && Input.GetKeyDown (KeyCode.Alpha1)) {
			audioScript.switch2 ();
			exLights = false;
			l_extLigtsR.enabled = false;
			l_extLigtsG.enabled = false;
			l_extLigtsB.enabled = false;
		}
		if (intLights == false&&power == true && Input.GetKeyDown (KeyCode.Alpha2)) { 
			next.text = "C";
			audioScript.switch2 ();
			intLights = true;
			l_intLights.enabled = true;
		}else if (intLights == true && Input.GetKeyDown (KeyCode.Alpha2)) {
			audioScript.switch2 ();
			intLights = false;
			l_intLights.enabled = false;
		}
		if (canopyClosed == false&&power == true && Input.GetKeyDown (KeyCode.C)) //Closing the canopy
		{
			next.text = "V";
			canopyClosed = true;//sound of closing canopy
			anim.SetTrigger ("T50Close");
			audioScript.switch3 ();
			surround.volume = 0.5f;
		} else if(canopyClosed == true &&Input.GetKeyDown(KeyCode.C)){
			canopyClosed = false;
			sealShip = false;
			anim.SetTrigger ("T50Open");
			audioScript.switch3 ();
			surround.volume = 1f;
		}
		if (sealShip==false&&power == true && canopyClosed == true && Input.GetKeyDown (KeyCode.V)) //Sealing the ship
		{
			next.text = "H";
			audioScript.switch3 ();
			sealShip = true; //Sound of ship getting sealed
		}else if(sealShip == true && Input.GetKeyDown(KeyCode.V)){
			audioScript.switch3 ();
			sealShip = false;
		}
		if (commsModOn == false && power == true && Input.GetKeyDown (KeyCode.H)) { //Communication Module On
			next.text = "F";
			audioScript.switch3 ();
			commsModOn = true;//sound of click
		} else if (commsModOn == true && Input.GetKeyDown (KeyCode.H)) {
			audioScript.switch3 ();
			commsModOn = false;
			freqFound = false;
			atcBool.firstpressed = false;
		}
		if (freqFound==false&&power == true&&commsModOn==true && Input.GetKeyDown (KeyCode.F)) //Finding Frequency
		{
			next.text = "Num1";
			audioScript.switch2 ();
			audioScript.v_fFound ();
			freqFound = true;//sound "Frequency Found"
		}
		if (fuelPumpFirst==false&&power == true && Input.GetKeyDown (KeyCode.Keypad1)) 
		{
			next.text = "Num2";
			audioScript.switch3 ();
			fuelPumpFirst = true;//sound of clicking, sound of fuel pump on
			peStart.clip = FuelPump;
			peStart.Play ();
		}else if(fuelPumpFirst == true && Input.GetKeyDown(KeyCode.Keypad1)){
			audioScript.switch3 ();
			fuelPumpFirst = false;
		}
		if (fuelPumpSecond==false&&power == true && fuelPumpFirst == true && Input.GetKeyDown (KeyCode.Keypad2)) 
		{
			next.text = "Num3";
			audioScript.switch3 ();
			fuelPumpSecond = true;//sound of clicking, sound of fuel pump on
			peStart.clip = FuelPump;
			peStart.Play ();
		}else if(fuelPumpSecond == true && Input.GetKeyDown(KeyCode.Keypad2)){
			audioScript.switch3 ();
			fuelPumpSecond = false;
		}
		if (fuelPumpThird == false&&power == true && fuelPumpFirst == true && fuelPumpSecond == true && Input.GetKeyDown (KeyCode.Keypad3)) 
		{
			next.text = "Num4";
			audioScript.switch3 ();
			fuelPumpThird = true; //sound of clicking, sound of fuel pump on
			peStart.clip = FuelPump;
			peStart.Play ();
		} else if(fuelPumpThird == true && Input.GetKeyDown(KeyCode.Keypad3)){
			audioScript.switch3 ();
			fuelPumpThird = false;
		}
		if (atcBool.firstpressed==true&&preheat == false && power == true && Input.GetKeyDown (KeyCode.Keypad4)) 
		{
			
			next.text = "Num5";
			audioScript.switch1 ();
			audioScript.v_reAr ();
			reactorArmed = true; //sound of clicking, reactor is armed
		}
		if (reactorFuelCell == false&&power == true&&reactorArmed==true&&fuelPumpThird==true&&Input.GetKeyDown(KeyCode.Keypad5))
		{
			next.text = "Num6";
			audioScript.switch3 ();
			reactorFuelCell = true; //sound of clicking
		}else if(reactorFuelCell == true && Input.GetKeyDown(KeyCode.Keypad5)){
			audioScript.switch3 ();
			reactorFuelCell = false;
			reactorOn = false;
		}
		if(reactorOn ==false&&reactorArmed== true&&reactorFuelCell==true&&power==true&&Input.GetKeyDown(KeyCode.Keypad6))
		{
			next.text = "L";
			audioScript.switch1 ();
			audioScript.v_reOn ();
			reactorOn = true;//Sound of clicking, reactor running, Sound "Reactor is online"
			reactorArmed = false;
			power = false;
		}else if(reactorOn == true && Input.GetKeyDown(KeyCode.Keypad6)){
			audioScript.switch1 ();
			audioScript.v_reOff ();
			reactorOn = false;
		}
		if (atcBool.secondpressed == true&&engineLock == true && reactorOn == true && Input.GetKeyDown (KeyCode.L)) {
			next.text = "K";
			engineLock = false;//sound of clicking, and Sound "Engine Unlocked, prepare for startup"
			audioScript.v_enUnl();
		} else if (engineLock == false && Input.GetKeyDown (KeyCode.L)) {
			engineLock = true;
			if (engineStart == true) {
				engineStart = false;//"Engine is shutting down"
				atcBool.secondpressed = false;
				engine1.enableEmission = false;
				engine2.enableEmission = false;
				audioScript.v_enOff ();
			}
		}
		if (engineStart == false && reactorOn == true && tempEng > 200.0 && Input.GetKeyDown (KeyCode.K)) {
			next.text = "Num7";
			engineStart = true; //sound of click and sound of engine start, and at the end sound "Engine Online"
			peStart.clip = EngineStart;
			peStart.Play ();
			audioScript.Invoke ("engineWorking", 7);
			audioScript.v_enOn ();
			Invoke ("emmission", 6);
		} else if (engineStart == true && Input.GetKeyDown (KeyCode.K)) {
			engineStart = false;//"Engine is shutting down"
			atcBool.secondpressed = false;
			engine1.enableEmission = false;
			engine2.enableEmission = false;
			audioScript.v_enOff ();
		}
		if (primaryDisplay==false&&reactorOn == true && Input.GetKeyDown (KeyCode.Keypad7)) {
			next.text = "Num8";
			primaryDisplay = true;//sound of clicking
			audioScript.switch3 ();
		}else if (primaryDisplay == true && Input.GetKeyDown (KeyCode.Keypad7)) {
			primaryDisplay = false;
			audioScript.switch3 ();
		}
		if (secondaryDisplay == false&&reactorOn == true && Input.GetKeyDown (KeyCode.Keypad8)) 
		{
			next.text = "Num9";
			secondaryDisplay = true; //sound of clicking
			audioScript.switch3 ();
		}else if (secondaryDisplay == true && Input.GetKeyDown (KeyCode.Keypad8)) {
			secondaryDisplay = false;
			audioScript.switch3 ();
		}
		if (headsUpDisplay == false&&reactorOn == true && Input.GetKeyDown (KeyCode.Keypad9)) 
		{
			next.text = "7";
			headsUpDisplay = true;
			audioScript.switch3 ();
		} else if (headsUpDisplay == true && Input.GetKeyDown (KeyCode.Keypad9)) {
			headsUpDisplay = false;
			audioScript.switch3 ();
		}
		if (probeHeat==false&&reactorOn == true && Input.GetKeyDown (KeyCode.Alpha7)) 
		{
			next.text = "4";
			probeHeat = true; //sound of clicking
			audioScript.switch3 ();
		} else if (probeHeat == true && Input.GetKeyDown (KeyCode.Alpha7)) {
			probeHeat = false;
			audioScript.switch3 ();
		}
		if (thrusters==false&&reactorOn == true && engineStart == true && Input.GetKeyDown (KeyCode.Alpha3))
		{
			thrusters = true;//sound of clicking
			audioScript.switch2 ();
		}else if (thrusters == true && Input.GetKeyDown (KeyCode.Alpha3)) {
			thrusters = false;
			audioScript.switch2 ();
		}
		if (hydraulicPumpOne == false&&reactorOn == true && engineStart == true && Input.GetKeyDown (KeyCode.Alpha4))
		{
			next.text = "5";
			audioScript.switch3 ();
			hydraulicPumpOne = true;//sound of clicking and sound of on
		}else if (hydraulicPumpOne == true && Input.GetKeyDown (KeyCode.Alpha4)) {
			hydraulicPumpOne = false;
			audioScript.switch3 ();
		}
		if (hydraulicPumpSecond == false&&reactorOn == true && engineStart == true &&hydraulicPumpOne==true&& Input.GetKeyDown (KeyCode.Alpha5))
		{
			next.text = "6";
			audioScript.switch3 ();
			hydraulicPumpSecond = true;//sound of clicking and sound of on
			spaceShipOn = true;//sound "all systems are online, ready to fly"
		}else if (hydraulicPumpSecond == true && Input.GetKeyDown (KeyCode.Alpha5)) {
			hydraulicPumpSecond = false;
			audioScript.switch3 ();
		}
		if (landingLights == false && reactorOn == true && Input.GetKeyDown (KeyCode.Alpha6)) {
			next.enabled = false;
			landingLights = true;//sound of clicking, and animation lights are on
			audioScript.switch2 ();
			l_lanLightsL.enabled = true;
			l_lanLightsR.enabled = true;
			audioScript.v_allDone ();
		} else if (landingLights == true && Input.GetKeyDown (KeyCode.Alpha6)) {
			landingLights = false;
			audioScript.switch2 ();
			l_lanLightsL.enabled = false;
			l_lanLightsR.enabled = false;
		}
		if (atcBool.thirdpressed == true&&parkingBreak == true&&spaceShipOn == true && Input.GetKeyDown (KeyCode.R)) {
			parkingBreak = false;
			//and allow the movement of the ship, only after this!!!
		}else if (parkingBreak == false && Input.GetKeyDown (KeyCode.R)) {
			parkingBreak = true;
			atcBool.thirdpressed = false;
			//cancell the movement of the ship!!
		}
		if (inertiaDumpener == false && Input.GetKeyDown (KeyCode.I)) {
			inertiaDumpener = true;
			audioScript.switch3 ();
		} else if (inertiaDumpener == true && Input.GetKeyDown (KeyCode.I)) {
			inertiaDumpener = false;
			audioScript.switch3 ();
		}
		if (gearUp == false && spaceShipOn == true && Input.GetKeyDown (KeyCode.G)) {
			gearUp = true;
			anim.SetTrigger ("GearUp");
			peStart.clip = GearUp;
			peStart.Play ();
		} else if (gearUp == true && Input.GetKeyDown (KeyCode.G)) {
			gearUp = false;
			anim.SetTrigger ("GearDown");
		}





	}
	private void enginePreheat()
	{
		tempPre.enabled = true;
		temp.enabled = true;
		temp.text = tempEng.ToString ();
		tempEng = tempEng + 0.2f;
	}
	private void emmission()
	{
		engine1.enableEmission = true;
		engine2.enableEmission = true;
	}
}
