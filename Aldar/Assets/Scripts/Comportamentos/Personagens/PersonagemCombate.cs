using UnityEngine;
using System.Collections;

public class PersonagemCombate : MonoBehaviour
{
	/*-------------- Combate ----------------*/
	public EnumTipoArmamento tipoArmamento;
    public Transform[] maos;
	public Transform[] slots;
	public Transform[] armas;

	/*-------------- Componentes e Controles ----------------*/
	private ControladorAnimator anim;
	private PosturaCombate combate;

	void Start ()
	{
		anim = gameObject.GetComponent<ControladorAnimator>();
		combate = ConstrutorPosturaCombate.Construir(this, tipoArmamento);
		combate.Iniciar();
	}

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

	public void Atacar()
	{
		if (anim.Postura != EnumEstadoArmamento.Armado)
			return;

		anim.Postura = EnumEstadoArmamento.Atacando;
        anim.PodeMover = false;
		StopAllCoroutines();
	}

	public void FinalizarAtaque(int ataque)
	{
		anim.Postura = EnumEstadoArmamento.Armado;
		float tempo = combate.FinalizarAtaque(ataque);
        StartCoroutine(VoltarMover(tempo));
	}

	private IEnumerator VoltarMover(float tempo)
	{
		yield return new WaitForSeconds(tempo);
		anim.PodeMover = true;
	}

	public void EmpunharArma()
	{
		bool armando = anim.Postura == EnumEstadoArmamento.Empunhando;
		anim.Postura = (armando) ? EnumEstadoArmamento.Armado : EnumEstadoArmamento.Desarmado;
		combate.EmpunharArma(armando);

		if (!anim.Combate)
			StartCoroutine(TrocarPostura());
	}
}
