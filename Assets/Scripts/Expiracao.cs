using UnityEngine;
using System.Collections;

public class Expiracao : MonoBehaviour {
	
	public float duracao;
	
	void Start ()
	{
		Destroy (gameObject, duracao);
	}
}
