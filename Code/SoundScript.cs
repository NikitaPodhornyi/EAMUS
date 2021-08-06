using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour {
	#region Declaration
	public AudioClip preheatRunning;
	public AudioClip preheatShuttingDown;
	public AudioClip EngineRunning;
	public AudioClip engFull;
	public AudioClip Switch1;
	public AudioClip Alarm;
	public AudioClip Switch2;
	public AudioClip Switch3;
	public AudioClip v_reOnline;
	public AudioClip v_frequencyFound;
	public AudioClip v_frequencyLost;
	public AudioClip v_reactorArmed;
	public AudioClip v_reactorOffline;
	public AudioClip v_engineUnlocked;
	public AudioClip v_engineOn;
	public AudioClip v_engineOff;
	public AudioClip v_systemOn;
	public AudioClip v_bBetty;
	public AudioClip atcReqCI;
	public AudioClip atcReqSC;
	public AudioClip atcReqTC;
	public AudioClip atcReqTOC;
	public AudioClip atcResCI;
	public AudioClip atcResSC;
	public AudioClip atcResTC;
	public AudioClip atcResTOC;
	public AudioSource audio3;
	public AudioSource voiceSounds;
	public AudioSource main;
	public AudioSource atcReq;
	public AudioSource atcRes;
	#endregion
	public void preheatWorking()
	{
		audio3.clip = preheatRunning;
		audio3.PlayDelayed (2);
		audio3.loop = true;
	}
	public void preheatStop()
	{
		audio3.clip = preheatShuttingDown;
		audio3.volume = 0.1f;
		audio3.loop = false;
		audio3.Play ();
	}
	public void engineWorking ()
	{
		audio3.clip = EngineRunning;
		audio3.loop = true;
		audio3.volume = 0.1f;
		audio3.Play ();
	}
	public void engineFull()
	{
		audio3.clip = engFull;
		audio3.loop = true;
		audio3.volume = 0.2f;
		audio3.Play ();
	}
	public void engineStop ()
	{
		
	}
	public void switch1 ()
	{
		main.clip = Switch1;
		main.loop = false;
		main.Play ();
	}
	public void switch2 ()
	{
		main.clip = Switch2;
		main.loop = false;
		main.Play ();
	}
	public void switch3 ()
	{
		main.clip = Switch3;
		main.loop = false;
		main.Play ();
	}
	public void v_reOn ()
	{
		voiceSounds.clip = v_reOnline;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (2);
	}
	public void v_fFound()
	{
		voiceSounds.clip = v_frequencyFound;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (1);
	}
	public void v_fLost ()
	{
		voiceSounds.clip = v_frequencyLost;
		voiceSounds.loop = false;
		voiceSounds.Play ();
	}
	public void v_reAr()
	{
		voiceSounds.clip = v_reactorArmed;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (1);
	}
	public void v_reOff()
	{
		voiceSounds.clip = v_reactorOffline;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (2);
	}
	public void v_enUnl ()
	{
		voiceSounds.clip = v_engineUnlocked;
		voiceSounds.loop = false;
		voiceSounds.Play ();
	}
	public void v_enOn ()
	{
		voiceSounds.clip = v_engineOn;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (8);
	}
	public void v_enOff ()
	{
		voiceSounds.clip = v_engineOff;
		voiceSounds.loop = false;
		voiceSounds.PlayDelayed (1);
	}
	public void v_allDone ()
	{
		voiceSounds.clip = v_systemOn;
		voiceSounds.loop = false;
		voiceSounds.Play ();
	}
	public void v_biBetty()
	{
		voiceSounds.clip = v_bBetty;
		voiceSounds.loop = false;
		voiceSounds.Play ();
	}
	public void reqCI()
	{
		atcReq.clip = atcReqCI;
		atcReq.loop = false;
		Invoke ("resCI", 5);
		atcReq.Play ();
	}
	private void resCI()
	{
		atcRes.clip = atcResCI;
		atcRes.loop = false;
		atcRes.Play ();
	}
	public void reqSC()
	{
		atcReq.clip = atcReqSC;
		atcReq.loop = false;
		Invoke ("resSC", 5);
		atcReq.Play ();
	}
	private void resSC()
	{
		atcRes.clip = atcResSC;
		atcRes.loop = false;
		atcRes.Play ();
	}
	public void reqTC()
	{
		atcReq.clip = atcReqTC;
		atcReq.loop = false;
		Invoke ("resTC", 6);
		atcReq.Play ();
	}
	private void resTC()
	{
		atcRes.clip = atcResTC;
		atcRes.loop = false;
		atcRes.Play ();
	}
	public void reqTOC()
	{
		atcReq.clip = atcReqTOC;
		atcReq.loop = false;
		Invoke ("resTOC", 8);
		atcReq.Play ();
	}
	private void resTOC()
	{
		atcRes.clip = atcResTOC;
		atcRes.loop = false;
		atcRes.Play ();
	}
}
