#pragma strict

var cam : GameObject;

public var intensity : float = 0.05;
var particules : GameObject;
var particules2 : GameObject;

function Start () {
particules.SetActive(false);
particules2.SetActive(false);
}

function ShakeCam () {
cam.SendMessage ("DoShake",intensity,SendMessageOptions.DontRequireReceiver);
particules.SetActive(true);
particules2.SetActive(true);
}