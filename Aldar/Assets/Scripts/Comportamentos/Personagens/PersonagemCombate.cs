using UnityEngine;
using System.Collections;

public class PersonagemCombate : MonoBehaviour
{
	/*-------------- Combate ----------------*/
	public EnumTipoArmamento tipoArmamento;
    public Transform[] Maos;	//parents para equipar as armas (EnumMaos)
	public Transform[] Slots;	//parents para guardar as armas (EnumSlots)
	public Transform[] Armas;	//prefabs das armas (EnumTipoArmamento)

	/*-------------- Componentes e Controles ----------------*/
	private ControladorAnimator Anim;
	private PosturaCombate Combate;

	void Start ()
	{
		Anim = gameObject.GetComponent<ControladorAnimator>();
		Combate = ConstrutorPosturaCombate.Construir(this, tipoArmamento);
		Combate.Iniciar(gameObject);
	}

	/// <summary>
	/// Coloca o personagem em postura de combate ou normal, guardando ou equipando seu armamento.
	/// </summary>
	public void TrocarPosturaCombate()
	{
		if (Anim.EmEscada || (Anim.Postura > EnumEstadoArmamento.Desarmado && Anim.Postura != EnumEstadoArmamento.Armado))
			return;

		Anim.Postura = (Anim.Postura == EnumEstadoArmamento.Desarmado) ?
			EnumEstadoArmamento.Empunhando : EnumEstadoArmamento.Guardando;
        Anim.Combate = !Anim.Combate;
		if (Anim.Combate)
			StartCoroutine(TrocarPostura());
	}

	/// <summary>
	/// Ativa/Desativa a camada de armamento do animator
	/// </summary>
	private IEnumerator TrocarPostura()
	{
		if(Anim.Combate)
		{
			for (int i = 0; i <= 10; i++)
			{
				Anim.AtivarCamadaArmas(i/10.0F);
				yield return new WaitForSeconds(0.025F);
			}
		}
		else
		{
			for (int i = 10; i >= 0; i--)
			{
				Anim.AtivarCamadaArmas(i / 10.0F);
				yield return new WaitForSeconds(0.025F);
			}
		}
	}

	/// <summary>
	/// Inicia a animação de ataque
	/// </summary>
	public void Atacar()
	{
		if (Anim.Postura != EnumEstadoArmamento.Armado)
			return;

		Anim.Postura = EnumEstadoArmamento.Atacando;
        Anim.PodeMover = false;
		StopAllCoroutines();
	}

	/// <summary>
	/// Essa função deve ser chamada de dentro da animação de ataque
	/// </summary>
	public void IniciarAtaque(int ataque)
	{
		Combate.IniciarAtaque(ataque);
	}

	/// <summary>
	/// Essa função deve ser chamada de dentro da animação de ataque
	/// </summary>
	public void FinalizarAtaque()
	{
		Anim.Postura = EnumEstadoArmamento.Armado;
		float tempo = Combate.FinalizarAtaque();
        StartCoroutine(VoltarMover(tempo));
	}

	/// <summary>
	/// Permite que o personagem possa se mover novamente ao finalizar um ataque
	/// </summary>
	private IEnumerator VoltarMover(float tempo)
	{
		yield return new WaitForSeconds(tempo);
		Anim.PodeMover = true;
	}

	/// <summary>
	/// Essa função deve ser chamada na animação de EmpunharArma
	/// </summary>
	public void EmpunharArma()
	{
		bool armando = Anim.Postura == EnumEstadoArmamento.Empunhando;
		Anim.Postura = (armando) ? EnumEstadoArmamento.Armado : EnumEstadoArmamento.Desarmado;
		Combate.EmpunharArma(armando);

		if (!Anim.Combate)
			StartCoroutine(TrocarPostura());
	}
}
