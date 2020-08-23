using UnityEngine;
using System.Collections;

public class Limites : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Tiro") {
						Destroy (other.gameObject, 2);

			Debug.Log("tiro destruido");
				}
			else
			Destroy (other.gameObject);
	}
}
