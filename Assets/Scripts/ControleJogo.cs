using UnityEngine;
using System.Collections;

public class ControleJogo : MonoBehaviour
{
	public float espera;

	public Inimigo inimigo;
	public Vector2 limitesInimigo;
	public float intervaloInimigo;

	public int pontosNivel;
	public float intervaloNivel;
	public float gravidadeNivel;

	private float placarNivel;

	private float gravidadeInimigo;

	public GUIText textoPlacar;
	public GUIText textoReinicio;
	public GUIText textoFimJogo;
	
	private bool fimJogo;
	private int placar;
	private bool reinicio;

	void Start ()
	{
		fimJogo = false;
		placar = 0;
		reinicio = false;
		placarNivel = 0;
		gravidadeInimigo = 0;

		textoFimJogo.text = "";
		textoReinicio.text = "";
		textoPlacar.text = "";

		StartCoroutine (GeraInimigos ());
	}
	
	void Update ()
	{
		if (reinicio && Input.GetKeyDown (KeyCode.X))
			Application.LoadLevel (Application.loadedLevel);
	}
	
	IEnumerator GeraInimigos ()
	{
		yield return new WaitForSeconds (espera);

		while (true) 
		{
			Vector3 posicaoInimigo = new Vector3 (Random.Range (-limitesInimigo.x, limitesInimigo.x), limitesInimigo.y, 0);

			Inimigo inimigoObject = Instantiate (inimigo, posicaoInimigo, Quaternion.identity) as Inimigo;

			Rigidbody2D inimigoRB = inimigoObject.GetComponent<Rigidbody2D> ();

			if(gravidadeInimigo == 0)
				gravidadeInimigo = inimigoRB.gravityScale;

			inimigoRB.gravityScale = gravidadeInimigo;

			yield return new WaitForSeconds (intervaloInimigo);

			if(fimJogo)
			{
				yield return new WaitForSeconds (espera);

				textoFimJogo.text = "GAME OVER";

				textoReinicio.text = "Aperte a tecla X para reiniciar";
	
				reinicio = true;

				break;
			}
		}
	}
	
	public void SomarPontos (int pontos)
	{
		placar += pontos;

		placarNivel += pontos;

		if (placarNivel >= pontosNivel) {
			for (int i = 0; i < pontosNivel; i += pontosNivel)
			{
				intervaloInimigo -= intervaloNivel;
				gravidadeInimigo += gravidadeNivel;
			}

			placarNivel = 0;
		}

		Debug.Log (intervaloInimigo);
		
		textoPlacar.text = placar.ToString();
	}
	
	public void FimJogo ()
	{
		fimJogo = true;

		audio.Stop ();
	}
}