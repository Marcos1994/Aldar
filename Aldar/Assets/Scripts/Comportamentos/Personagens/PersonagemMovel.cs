using UnityEngine;
using System.Collections;

public class PersonagemMovel : MonoBehaviour
{
	/*-------------- Movimentação ----------------*/
	[Range(0, 6)] public float VelocidadeMovimento = 4;
	[Range(0, 100)] public float ForcaPulo = 55;
	public float xMov = 0, yMov = 0;
	public int SentidoX = 0, SentidoY = 1;
	public Vector3 ForcaAdicional = Vector3.zero;
	public Transform EscadaProxima;
	private float RaioCC;

	/*-------------- Componentes e Controles ----------------*/
	private CharacterController cc;	//Center.Y = 0,95; Radius = 0,25; Height = 1,8
	private RaycastHit raycastHit;
	private ControllerColliderHit colliderHit;
	private float rayDistance;
	private ControladorAnimator anim;
	
	void Start()
	{
		anim = gameObject.GetComponent<ControladorAnimator>();
		cc = gameObject.GetComponent<CharacterController>();
		rayDistance = cc.height/2 + cc.radius;
		RaioCC = cc.radius;
    }

	void Update()
	{
		//Define se o personagem está ou não no chão
		anim.NoChao = (cc.collisionFlags & CollisionFlags.Below) != 0 ||
					  (cc.collisionFlags == CollisionFlags.Below);
		
		//Aceleração da gravidade
		anim.Gravidade = (!anim.NoChao && cc.collisionFlags == CollisionFlags.Above) ?
			-0.1F : Mathf.Clamp(anim.Gravidade - (Time.deltaTime / 2), ((anim.NoChao || cc.velocity.y == 0) ? -0.3F : -0.6F), 0.5F);

		//Execução da movimentação
		if (anim.EmEscada)
			cc.Move(new Vector3(0, yMov, 0));
		else
			cc.Move(ForcaAdicional + new Vector3(xMov, anim.Gravidade, yMov));
	}

	//Muda o estado da possibilidade de movimentação do personagem
	public void PodeMover(int podeMover)
	{
		anim.PodeMover = (podeMover == 1);
	}

	//Reduz a velocidade do personagem caso ele comece a andar
	public void Andar(bool andando)
	{
		anim.Andando = andando;
	}

	//Gerencia o comportamento de movimentação do personagem
	public void Mover(float x, float y)
	{
		if (!anim.PodeMover)
		{
			anim.Velocidade = 0;
			this.xMov = this.yMov = 0;
			if(anim.Postura > EnumEstadoArmamento.Armado)
				Rotacionar(x, y);
			return;
		}

		if (anim.EmEscada)
			Escalar(y);
		else
			Correr(x,y);

		cc.radius = RaioCC + (RaioCC * anim.Velocidade);
	}

	//Gerencia o comportamento de movimentação do personagem NO CHÃO
	private void Correr(float x, float y)
	{
		float slideSpeed = 1;

		if (anim.NoChao)
		{
			if (!anim.Andando)
			{
				//Checa se está em ladeira
				Physics.Raycast(gameObject.transform.position, -Vector3.up, out raycastHit, rayDistance);
				slideSpeed = (90 - Vector3.Angle(raycastHit.normal, Vector3.up)) / 90;
			}
			else if (!anim.EmEscada)
				slideSpeed = 0.4F;
		}
		float multiplicador = VelocidadeMovimento * slideSpeed * Time.deltaTime;
		xMov = x * multiplicador;
		yMov = y * multiplicador;
		anim.Velocidade = (x != 0 || y != 0) ? 1 : 0;
		Rotacionar(x, y);
	}

	//Altera a gravidade do personagem para q ele pule
	public void Pular()
	{
		if (anim.PodeMover && (anim.NoChao || anim.EmEscada))
		{
			if (anim.EmEscada)
			{
				anim.Gravidade = 0.15F;
				StartCoroutine(ParaEscada(false));
				return;
			}
			anim.Gravidade = ForcaPulo / 300;
		}
	}

	//Animação de entrar em escada
	public void EntrarEscada()
	{
		if (anim.PodeMover && !anim.EmEscada && anim.NoChao && !anim.Combate && EscadaProxima != null)
		{
			float distancia = EscadaProxima.position.x - gameObject.transform.position.x;
			if (distancia > -0.25F && distancia < 0.25F)
			{
				Debug.Log("Começou");
				anim.PodeMover = false;
				StartCoroutine(ParaEscada(true));
			}
		}
	}

	//Animação do personagem entrando na escada.
	private IEnumerator ParaEscada(bool entrar)
	{
		if (entrar) anim.PodeMover = false;
		anim.EmEscada = entrar;
		
		if (entrar)
			gameObject.transform.eulerAngles = EscadaProxima.transform.eulerAngles;

		yield return new WaitForSeconds((entrar) ? 0.4F : 0);
		anim.PodeMover = true;
	}

	//Gerencia o comportamento de movimentação do personagem EM ESCADA
	public void Escalar(float y)
	{
		if (anim.PodeMover)
		{
			yMov = y;
            if (yMov != 0)
			{
				if (yMov > 0 && EscadaProxima == null)
					yMov = 0;
				else if (anim.NoChao)
					StartCoroutine(ParaEscada(false));
			}
			xMov = 0;
			anim.Velocidade = yMov;
			yMov = yMov * VelocidadeMovimento * Time.deltaTime / 3;
		}
	}

	//Gerencia o direcionamento do personagem
	private void Rotacionar(float x, float y)
	{
		if ((SentidoX == x && SentidoY == y) || (x == 0 && y == 0))
			return;
		SentidoX = SentidoY = 0;
		if (x != 0) SentidoX = (x > 0) ? 1 : -1;
		if (y != 0) SentidoY = (y > 0) ? 1 : -1;

		if (anim.EmEscada)
			return;

		//ang	x  y
		//45   +1 +1
		//90   +1  0
		//135  +1 -1
		//0		0 +1
		//180	0 -1
		//225  -1 -1
		//270  -1  0
		//315  -1 +1

		int angulo = 0;
		if (x == 0)		angulo = (SentidoY > 0) ? 0 : 180;
		else if (x > 0) angulo = 180 - (90 * SentidoX) - (45 * SentidoY);
		else			angulo = 180 - (90 * SentidoX) + (45 * SentidoY);

		gameObject.transform.eulerAngles = new Vector2(0, angulo);
	}

	//private IEnumerator InverterSentido()
	//{
	//	anim.PodeMover = false;
	//	float espera = (anim.NoChao) ? 0.1F : 0F;
	//	yield return new WaitForSeconds(espera);
	//	sentido *= -1;
	//	gameObject.transform.eulerAngles = new Vector2(0, 180 - (sentido * 90));
	//	anim.PodeMover = true;
	//}
}
