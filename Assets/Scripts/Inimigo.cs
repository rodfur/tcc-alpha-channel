using UnityEngine;
using System.Collections;

public class Inimigo : MonoBehaviour {

	public GameObject explosao;
	public GameObject explosaoPersonagem;
	public int numeroPontos;
	private ControleJogo controleJogo;
	private Personagem personagem;
	
	void Start ()
	{
		GameObject ControleJogoObject = GameObject.FindGameObjectWithTag ("GameController");

		controleJogo = ControleJogoObject.GetComponent <ControleJogo>();
	
		GameObject PersonagemObject = GameObject.FindGameObjectWithTag ("Player");

		personagem = PersonagemObject.GetComponent <Personagem>();
	}
	
	void OnTriggerEnter2D (Collider2D other)
	{
		switch (other.tag)
		{
			case "Player":

				float yRot;

				switch(personagem.Posicao)
				{
					case PosicaoPersonagem.ParadoDireita:
					case PosicaoPersonagem.AcimaDireita:
					case PosicaoPersonagem.CorrendoDireita:
					case PosicaoPersonagem.DiagonalDireita:
						yRot = 180;
						break;
					default:
						yRot = 0;
						break;
				}

				controleJogo.FimJogo ();

				Instantiate (explosaoPersonagem, other.transform.position, Quaternion.Euler(new Vector3(0,yRot,0)));

				GameObject[] inimigos = GameObject.FindGameObjectsWithTag("Inimigo");

				for(int i = 0; i < inimigos.Length; i++)
					Destroy(inimigos[i]);

				Destroy (other.gameObject);

				break;

			case "Tiro":
				Destroy (other.gameObject);
				controleJogo.SomarPontos(numeroPontos);
				Instantiate(explosao, transform.position, transform.rotation);
				break;

			case "Chao":
				break;

			default:
				return;
		}

		Destroy (gameObject);

	}
}
