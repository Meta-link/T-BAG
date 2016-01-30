using UnityEngine; using System.Collections;

public class ShakeCamera : MonoBehaviour { 

	public bool Shaking; 
	private float ShakeDecay; 
	private float ShakeIntensity;
	private Vector3 OriginalPos;
	private Quaternion OriginalRot;
	public float Intensity = 0.05f;
	
	void Start()
	{
		Shaking = false;
		OriginalRot = transform.rotation;

	}
	
	
	// Update is called once per frame
	void Update ()
	{
		if(ShakeIntensity > 0)
		{

			transform.rotation = new Quaternion(OriginalRot.x + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.y + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.z + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f,
			                                    OriginalRot.w + Random.Range(-ShakeIntensity, ShakeIntensity)*.2f);
			
			ShakeIntensity -= ShakeDecay;
		}
		else if (Shaking)
		{
			Shaking = false;
		}

	}


	/*void OnGUI() {
		
		if (GUI.Button(new Rect(10, 200, 50, 30), "Shake"))
			DoShake(Intensity);
	} */

	public void DoShake(float Intensity)
	{


		
		ShakeIntensity = Intensity;
		ShakeDecay = 0.001f;
		Shaking = true;
	}
	
}