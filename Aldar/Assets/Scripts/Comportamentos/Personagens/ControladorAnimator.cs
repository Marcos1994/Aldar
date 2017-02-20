using UnityEngine;
using System.Collections;

/// <summary>
/// Script responsavel pela maquina de estado das animações
/// </summary>
public class ControladorAnimator : MonoBehaviour
{
	private Animator Anim;
	private bool combate, andando, emEscada, noChao, podeMover;
	private float velocidade, gravidade;
	private int postura;

	void Start()
	{
		Anim = gameObject.GetComponent<Animator>();

		combate = Anim.GetBool("Combate");
		andando = Anim.GetBool("Andando");
		emEscada = Anim.GetBool("EmEscada");
		noChao = Anim.GetBool("NoChao");

		velocidade = Anim.GetFloat("Velocidade");
		gravidade = Anim.GetFloat("Gravidade");

		postura = Anim.GetInteger("Postura");

		podeMover = true;
	}

	public void Tedio()
	{
		if (Random.Range(1, 10) > 7)
			Anim.Play("Tedio");
	}

	public bool PodeMover
	{
		get { return podeMover; }
		set { podeMover = value; }
	}

	public bool Combate
	{
		get { return combate; }
		set { if (combate != value) Anim.SetBool("Combate", (combate = value)); }
	}

	public bool EmEscada
	{
		get { return emEscada; }
		set { if (emEscada != value) Anim.SetBool("EmEscada", (emEscada = value)); }
	}

	public bool Andando
	{
		get { return andando; }
		set { if (andando != value) Anim.SetBool("Andando", (andando = value)); }
	}

	public float Velocidade
	{
		get { return velocidade; }
		set { if (velocidade != value) Anim.SetFloat("Velocidade", velocidade = value); }
	}

	public float Gravidade
	{
		get { return gravidade; }
		set { if (gravidade != value) Anim.SetFloat("Gravidade", gravidade = value); }
	}

	public bool NoChao
	{
		get { return noChao; }
		set { if (noChao != value) Anim.SetBool("NoChao", noChao = value); }
	}

	public EnumEstadoArmamento Postura
	{
		get { return (EnumEstadoArmamento)postura; }
		set { if (postura != (int)value) Anim.SetInteger("Postura", postura = (int) value); }
	}

	public ControladorAnimator(Animator anim)
	{
		this.Anim = anim;
	}

	public void AtivarCamadaArmas(float peso)
	{
		Anim.SetLayerWeight(2, peso);
	}
}
