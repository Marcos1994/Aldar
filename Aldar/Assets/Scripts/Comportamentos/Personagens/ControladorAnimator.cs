using UnityEngine;
using System.Collections;

/// <summary>
/// Script responsavel pela maquina de estado das animações
/// </summary>
public class ControladorAnimator : MonoBehaviour
{
	private Animator anim;
	private bool combate, andando, emEscada, noChao, podeMover;
	private float velocidade, gravidade;
	private int postura;

	void Start()
	{
		anim = gameObject.GetComponent<Animator>();

		combate = anim.GetBool("Combate");
		andando = anim.GetBool("Andando");
		emEscada = anim.GetBool("EmEscada");
		noChao = anim.GetBool("NoChao");

		velocidade = anim.GetFloat("Velocidade");
		gravidade = anim.GetFloat("Gravidade");

		postura = anim.GetInteger("Postura");

		podeMover = true;
	}

	public void Tedio()
	{
		if (Random.Range(1, 10) > 7)
			anim.Play("Tedio");
	}

	public bool PodeMover
	{
		get { return podeMover; }
		set { podeMover = value; }
	}

	public bool Combate
	{
		get { return combate; }
		set { if (combate != value) anim.SetBool("Combate", (combate = value)); }
	}

	public bool EmEscada
	{
		get { return emEscada; }
		set { if (emEscada != value) anim.SetBool("EmEscada", (emEscada = value)); }
	}

	public bool Andando
	{
		get { return andando; }
		set { if (andando != value) anim.SetBool("Andando", (andando = value)); }
	}

	public float Velocidade
	{
		get { return velocidade; }
		set { if (velocidade != value) anim.SetFloat("Velocidade", velocidade = value); }
	}

	public float Gravidade
	{
		get { return gravidade; }
		set { if (gravidade != value) anim.SetFloat("Gravidade", gravidade = value); }
	}

	public bool NoChao
	{
		get { return noChao; }
		set { if (noChao != value) anim.SetBool("NoChao", noChao = value); }
	}

	public EnumEstadoArmamento Postura
	{
		get { return (EnumEstadoArmamento)postura; }
		set { if (postura != (int)value) anim.SetInteger("Postura", postura = (int) value); }
	}

	public ControladorAnimator(Animator anim)
	{
		this.anim = anim;
	}

	public void AtivarCamadaArmas(float peso)
	{
		anim.SetLayerWeight(2, peso);
	}
}
