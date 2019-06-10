using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PluginWrapper : MonoBehaviour {

    private AndroidJavaObject javaClass;
    public Text myText;
	public Text myText2;



	// Use this for initialization
	void Start () {
        javaClass = new AndroidJavaObject("com.example.vrlibrary.Keys");

		Physics.IgnoreLayerCollision(9, 8);
		Physics.IgnoreLayerCollision(8, 8);
	

	}

	public GameObject szRatte;
	public GameObject szAmeise;
	public GameObject szEule;
	public GameObject szVogel;
	public GameObject szEich;
	public GameObject fertig;
	bool cansnap = false;
	// Update is called once per frame
	void Update () {
		if (objectA != null) {
			
			if (objectA.name == "Ratte") {
				szAmeise.SetActive (false);
				szRatte.SetActive (true);
				szRatte.GetComponent<snap_allowed> ().enabled = true;
			} else {
				szRatte.GetComponent<snap_allowed> ().enabled = false;
			}
			if (objectA.name == "Ameise") {
				szRatte.SetActive (false);
				szAmeise.SetActive (true);
				szAmeise.GetComponent<snap_allowed> ().enabled = true;
			} else {
				szAmeise.GetComponent<snap_allowed> ().enabled = false;
			}
			if (objectA.name == "Eule") {
				szEule.SetActive (true);
				szEule.GetComponent<snap_allowed> ().enabled = true;
				szEich.SetActive (false);
				szVogel.SetActive (false);
			} else {
				szEule.GetComponent<snap_allowed> ().enabled = false;
			}
			if (objectA.name == "Vogel") {
				szVogel.SetActive (true);
				szVogel.GetComponent<snap_allowed> ().enabled = true;
				szEich.SetActive (false);
				szEule.SetActive (false);
			} else {
				szVogel.GetComponent<snap_allowed> ().enabled = false;
			}
			if (objectA.name == "Eichhörnchen") {
				szEich.SetActive (true);
				szEich.GetComponent<snap_allowed> ().enabled = true;
				szVogel.SetActive (false);
				szEule.SetActive (false);
			} else {
				szEich.GetComponent<snap_allowed> ().enabled = false;
			}
		}

		if (snapzo != null) {
			cansnap = snapzo.GetComponent<snap_allowed> ().snapallow;
		}

		if ( (hirschsnap ==true) && (eichsnap == true) && ( eulesnap == true) && (rattesnap == true) && (vogelsnap == true) && (ameisesnap == true)){
			fertig.SetActive(true);

		}

		//für Aufgabe 2 jeweils die SnapZone des jeweiligen Stern aktiv setzen, wenn dieser gegriffen

    }




	public GameObject hand;
	public GameObject objectA;
	public Transform objectB;
	public bool angeschaut=false;

	public bool snapallowed;
	Vector3 npos;


	public void greifen(string ok){
		myText.text = "greifen"+ok;
		//myText.text = "lauter";
		if ((ok == "1") && (angeschaut == true)) {
			if (hand.transform.childCount == 0) {
				myText.text = "lauter";
				objectA.transform.position = objectB.position;
				objectA.transform.rotation = Quaternion.Euler(0,0,0);
				if (objectA.name == "Hirsch"){
					objectA.transform.localScale = new Vector3 (0.1f, 0.1f, 0.1f);
					objectA.transform.rotation = Quaternion.Euler(-90,0,90);
				}
				if (objectA.name == "Eichhörnchen"){
					objectA.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					objectA.transform.rotation = Quaternion.Euler(-90,0,0);
				}
				if (objectA.name == "Eule"){
					objectA.transform.localScale = new Vector3 (0.5f, 0.5f, 0.5f);
					objectA.transform.rotation = Quaternion.Euler(-90,0,0);
				}
				if (objectA.name == "Ratte"){
					objectA.transform.rotation = Quaternion.Euler(-90,0,90);
					objectA.transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
				}
				objectA.transform.parent = objectB;
				objectA.GetComponent<Rigidbody>().useGravity = false;


			
			}

			}
		else if ((ok == "1")&&(hand.transform.childCount == 1)){
			getpospointer ();


			hand.transform.DetachChildren ();
			objectA.GetComponent<Rigidbody> ().useGravity = true;
			objectA.transform.localScale = new Vector3 (1f, 1f, 1f);
			npos.x = wpos.x;
			npos.z = wpos.z;
			npos.y = wpos.y+0.1f;
			if (objectA.name == "Hirsch"){
				npos.y = wpos.y+0.9f;
			}

			objectA.transform.position = npos;
			if ((snapallowed = true)&&(objecttosnap==objectA)&&(cansnap == true)) {
				snap ();
			} else {
					objectA.GetComponent<Rigidbody> ().useGravity = true;
				}
		}
	}


	public GameObject snapzo;
	public GameObject objecttosnap;
	public GameObject snappos;
	bool enter=true;
	bool hirschsnap;
	bool eichsnap;
	bool eulesnap;
	bool rattesnap;
	bool ameisesnap;
	bool vogelsnap;

	public void snap(){
		

	//	objectA.GetComponent<Rigidbody> ().useGravity = true;
		OnTriggerStay (snapzo.GetComponent<Collider> ());

		if (objecttosnap.name == "Hirsch") {
			hirschsnap = true;
		}

		if (objecttosnap.name == "Eichhörnchen") {
			eichsnap = true;
		}
			
		if (objecttosnap.name == "Eule") {
			eulesnap = true;
		}

		if (objecttosnap.name == "Ratte") {
			rattesnap = true;
		}

		if (objecttosnap.name == "Ameise") {
			ameisesnap = true;
		}

		if (objecttosnap.name == "Vogel") {
			vogelsnap = true;
		}
	}



	void OnTriggerStay(Collider other)
	{
		if( (enter)&& (snapallowed)) {
			objectA.transform.Find ("Pfeil").gameObject.SetActive (true);
			objectA.GetComponent<Rigidbody> ().useGravity = false;
			objectA.GetComponent<Collider> ().enabled = false;
			objectA.transform.rotation = snappos.transform.rotation;
			objectA.transform.position = snappos.transform.position;
			objectA.GetComponent<Rigidbody> ().constraints = RigidbodyConstraints.FreezeAll;
			objectA.transform.localScale = snappos.transform.localScale;

		} 
	}

	public void anschauen(){
		angeschaut = true;
	}

	public void wegschauen(){
		angeschaut = false;
	}

	public GameObject pointer;
	public Vector3 wpos;

	public void getpospointer(){
		wpos = pointer.GetComponent<GvrReticlePointer> ().CurrentRaycastResult.worldPosition;
	
	}

	GameObject oA;
	bool panelactive=false;
	public GameObject camera;
	public void infopanel(string oki) {
		/*if ((oki == "1")&&(hand.transform.childCount == 1)&&(panelactive == false)){
			oA = objectA.transform.Find ("PanelMenu").gameObject;
			oA.SetActive (true);
			panelactive = true;
			oA.transform.LookAt (camera.transform);
		
			
		} else	if ((panelactive == true)&&(hand.transform.childCount == 1)&&(oki == "1")){
			oA.SetActive (false);
			panelactive = false;




	}
*/

		


	}



}
