using UnityEngine;
using System.Collections;

public class PersonagemCombate : MonoBehaviour
{
	/*-------------- Combate ----------------*/
	public EnumTipoArmamento tipoArmamento;
    public Transform[] maos;	//parents para equipar as armas (EnumMaos)
	public Transform[] slots;	//parents para guardar as armas (EnumSlots)
	public Transform[] armas;	//prefabs das armas (EnumTipoArmamento)

	/*-------------- Componentes e Controles ----------------*/
	private ControladorAnimator anim;
	private PosturaCombate combate;

	void Start ()
	{
		anim = gameObject.GetComponent<ControladorAnimator>();
		combate = ConstrutorPosturaCombate.Construir(this, tipoArmamento);
		combate.Iniciar();
	}

	/// <summary>
	/// Coloca o personagem em postura de combate ou normal, guardando ou equipando seu armamento.
	/// </summary>
	public void TrocarPosturaCombate()
	{
		if (anim.EmEscada || (anim.Postura > EnumEstadoArmamento.Desarmado && anim.Postura != EnumEstadoArmamento.Armado))
			return;

		anim.Postura = (anim.Postura == EnumEstadoArmamento.Desarmado) ?
			EnumEstadoArmamento.Empunhando : EnumEstadoArmamento.Guardando;
        anim.Combate = !anim.Combate;
		if (anim.Combate)
			StartCoroutine(TrocarPostura());
	}

	/// <summary>
	/// Ativa/Desativa a camada de armamento do animator
	/// </summary>
	private IEnumerator TrocarPostura()
	{
		if(anim.Combate)
		{
			for (int i = 0; i <= 10; i++)
			{
				anim.AtivarCamadaArmas(i/10.0F);
				yield return new WaitForSeconds(0.025F);
			}
		}
		else
		{
			for (int i = 10; i >= 0; i--)
			{
				anim.AtivarCamadaArmas(i / 10.0F);
				yield return new WaitForSeconds(0.025F);
			}
		}
	}

	/// <summary>
	/// Inicia a animação de ataque
	/// </summary>
	public void Atacar()
	{
		if (anim.Postura != EnumEstadoArmamento.Armado)
			return;

		anim.Postura = EnumEstadoArmamento.Atacando;
        anim.PodeMover = false;
		StopAllCoroutines();
	}

	/// <summary>
	/// Essa função deve ser chamada de dentro da animação de ataque
	/// </summary>
	public void FinalizarAtaque(int ataque)
	{
		anim.Postura = EnumEstadoArmamento.Armado;
		float tempo = combate.FinalizarAtaque(ataque);
        StartCoroutine(VoltarMover(tempo));
	}

	/// <summary>
	/// Permite que o personagem possa se mover novamente ao finalizar um ataque
	/// </summary>
	private IEnumerator VoltarMover(float tempo)
	{
		yield return new WaitForSeconds(tempo);
		anim.PodeMover = true;
	}

	/// <summary>
	/// Essa função deve ser chamada na animação de EmpunharArma
	/// </summary>
	public void EmpunharArma()
	{
		bool armando = anim.Postura == EnumEstadoArmamento.Empunhando;
		anim.Postura = (armando) ? EnumEstadoArmamento.Armado : EnumEstadoArmamento.Desarmado;
		combate.EmpunharArma(armando);

		if (!anim.Combate)
			StartCoroutine(TrocarPostura());
	}
}
