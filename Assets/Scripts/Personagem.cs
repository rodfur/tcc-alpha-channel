using UnityEngine;
using System.Collections;

public enum PosicaoPersonagem
{
	Descanso, 
	ParadoEsquerda, CorrendoEsquerda, AcimaEsquerda, DiagonalEsquerda, 
	ParadoDireita, CorrendoDireita, AcimaDireita, DiagonalDireita
}

[System.Serializable]
public class PosicaoTiro
{
	public Vector2 paradoEsquerda;
	public Vector2 correndoEsquerda;
	public Vector2 diagonalEsquerda;
	public Vector2 acimaEsquerda;
	public Vector2 paradoDireita;
	public Vector2 correndoDireita;
	public Vector2 diagonalDireita;
	public Vector2 acimaDireita;

}

public class Personagem : MonoBehaviour
{
	public float velocidadePersonagem;
	public float xMin, xMax;
	public float tempoParaDescanso;
	public GameObject tiro;
	public float velocidadeTiro;
	public float tempoDisparo;
	public PosicaoTiro posicaoTiro;


	[HideInInspector]
	public PosicaoPersonagem Posicao;

	private Animator anim;
	private float descanso;
	private float proximoTiro;

	void Awake ()
	{
		anim = GetComponent<Animator> ();

		Posicao = PosicaoPersonagem.Descanso;
	}

	void Update ()
	{
		float h = Input.GetAxis ("Horizontal");
		
		if (h == 0) 
		{
			if(Input.GetKey (KeyCode.UpArrow) || Input.GetKey(KeyCode.X))
				descanso = Time.time + tempoParaDescanso;

			if (Posicao == PosicaoPersonagem.Descanso && Input.GetKey (KeyCode.UpArrow))
				Posicao = PosicaoPersonagem.AcimaEsquerda;

			if (Posicao == PosicaoPersonagem.Descanso && Input.GetKey (KeyCode.X))
				Posicao = PosicaoPersonagem.ParadoEsquerda;

			if (Posicao == PosicaoPersonagem.DiagonalDireita)
				Posicao = PosicaoPersonagem.ParadoDireita;

			if (Posicao == PosicaoPersonagem.CorrendoDireita) 
				Posicao = PosicaoPersonagem.ParadoDireita;

			if (Posicao == PosicaoPersonagem.ParadoDireita && Input.GetKey (KeyCode.UpArrow))
				Posicao = PosicaoPersonagem.AcimaDireita;

			if(Posicao == PosicaoPersonagem.AcimaDireita && !Input.GetKey (KeyCode.UpArrow))
				Posicao = PosicaoPersonagem.ParadoDireita;

			if (Posicao == PosicaoPersonagem.DiagonalEsquerda)
				Posicao = PosicaoPersonagem.ParadoEsquerda;

			if (Posicao == PosicaoPersonagem.CorrendoEsquerda) 
				Posicao = PosicaoPersonagem.ParadoEsquerda;

			if (Posicao == PosicaoPersonagem.ParadoEsquerda && Input.GetKey (KeyCode.UpArrow))
				Posicao = PosicaoPersonagem.AcimaEsquerda;

			if(Posicao == PosicaoPersonagem.AcimaEsquerda && !Input.GetKey (KeyCode.UpArrow))
				Posicao = PosicaoPersonagem.ParadoEsquerda;

			switch(Posicao)
			{
				case PosicaoPersonagem.ParadoEsquerda:
					anim.SetTrigger ("PararEsquerda");
					break;
				case PosicaoPersonagem.AcimaEsquerda:
					anim.SetTrigger ("PararAcimaEsquerda");
					break;
				case PosicaoPersonagem.AcimaDireita:
					anim.SetTrigger ("PararAcimaDireita");
					break;
				case PosicaoPersonagem.ParadoDireita:
					anim.SetTrigger ("PararDireita");
					break;
				default:
					anim.SetTrigger ("PararFrente");
					break;
			}
		}

		if (h > 0) 
		{
			descanso = Time.time + tempoParaDescanso;
			
			if (Input.GetKey (KeyCode.UpArrow)) 
			{
				Posicao = PosicaoPersonagem.DiagonalDireita;	
				
				anim.SetTrigger ("CorrerDiagonalDireita");
			} 
			else
			{
				Posicao = PosicaoPersonagem.CorrendoDireita;
				
				anim.SetTrigger ("CorrerDireita");
			}
		}

		if (h < 0) 
		{
			descanso = Time.time + tempoParaDescanso;
			
			if (Input.GetKey (KeyCode.UpArrow)) 
			{
				Posicao = PosicaoPersonagem.DiagonalEsquerda;	
				
				anim.SetTrigger ("CorrerDiagonalEsquerda");
			} 
			else
			{
				Posicao = PosicaoPersonagem.CorrendoEsquerda;
				
				anim.SetTrigger ("CorrerEsquerda");
			}
		}

		if (Time.time > descanso)
			Posicao = PosicaoPersonagem.Descanso;
	}

	void FixedUpdate ()
	{
		float angTiro, xPosTiro, yPosTiro, xVelTiro, yVelTiro;

		rigidbody2D.velocity = (new Vector2 (Input.GetAxis ("Horizontal"), 0)) * velocidadePersonagem;

		rigidbody2D.position = new Vector2 
		(
			Mathf.Clamp (rigidbody2D.position.x, xMin, xMax), rigidbody2D.position.y
		);

		if (Input.GetKey (KeyCode.X) && Posicao != PosicaoPersonagem.Descanso && Time.time > proximoTiro)
		{
			proximoTiro = Time.time + tempoDisparo;

			switch(Posicao)
			{
			case PosicaoPersonagem.ParadoEsquerda:
				angTiro = 180;
				xPosTiro = posicaoTiro.paradoEsquerda.x;
				yPosTiro = posicaoTiro.paradoEsquerda.y;
				xVelTiro = -velocidadeTiro;
				yVelTiro = 0;
				break;
			case PosicaoPersonagem.CorrendoEsquerda:
				angTiro = 180;
				xPosTiro = posicaoTiro.correndoEsquerda.x;
				yPosTiro = posicaoTiro.correndoEsquerda.y;
				xVelTiro = -velocidadeTiro;
				yVelTiro = 0;
				break;
			case PosicaoPersonagem.DiagonalEsquerda:
				angTiro = 135;
				xPosTiro = posicaoTiro.diagonalEsquerda.x;
				yPosTiro = posicaoTiro.diagonalEsquerda.y;
				xVelTiro = -velocidadeTiro;
				yVelTiro = velocidadeTiro;
				break;
			case PosicaoPersonagem.AcimaEsquerda:
				angTiro = 90;
				xPosTiro = posicaoTiro.acimaEsquerda.x;
				yPosTiro = posicaoTiro.acimaEsquerda.y;
				xVelTiro = 0;
				yVelTiro = velocidadeTiro;
				break;
			case PosicaoPersonagem.AcimaDireita:
				angTiro = 90;
				xPosTiro = posicaoTiro.acimaDireita.x;
				yPosTiro = posicaoTiro.acimaDireita.y;
				xVelTiro = 0;
				yVelTiro = velocidadeTiro;
				break;
			case PosicaoPersonagem.DiagonalDireita:
				angTiro = 45;
				xPosTiro = posicaoTiro.diagonalDireita.x;
				yPosTiro = posicaoTiro.diagonalDireita.y;
				xVelTiro = velocidadeTiro;
				yVelTiro = velocidadeTiro;
				break;
			case PosicaoPersonagem.CorrendoDireita:
				angTiro = 0;
				xPosTiro = posicaoTiro.correndoDireita.x;
				yPosTiro = posicaoTiro.correndoDireita.y;
				xVelTiro = velocidadeTiro;
				yVelTiro = 0;
				break;
			case PosicaoPersonagem.ParadoDireita:
				angTiro = 0;
				xPosTiro = posicaoTiro.paradoDireita.x;
				yPosTiro = posicaoTiro.paradoDireita.y;
				xVelTiro = velocidadeTiro;
				yVelTiro = 0;
				break;
			default:
				return;
			}
			GameObject tiroObject = Instantiate(
												tiro, 
												new Vector3(transform.position.x + xPosTiro, transform.position.y + yPosTiro),
												Quaternion.Euler(new Vector3(0,0,angTiro))
												) 
												as GameObject;
		
			Rigidbody2D tiroRB = tiroObject.GetComponent<Rigidbody2D> ();

			tiroRB.velocity = new Vector2(xVelTiro,yVelTiro);
		}
	}
}
