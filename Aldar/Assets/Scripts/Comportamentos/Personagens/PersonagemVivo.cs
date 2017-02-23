using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemVivo : MonoBehaviour
{
	[HideInInspector]
	public DanoTexto PrefabNormal;
	[HideInInspector]
	public DanoTexto PrefabCritico;
	private Transform Canvas;
	private Camera Camera;
	private ControladorAnimator Anim;

	/// <summary>
	/// Dano reduz Vida e pode reduzir Vigor (dano físico) e/ou Consciência (dano psicológico).
	/// Vida = 0, morto; Vigor = 0, mais lento e menos preciso; Consciencia = 0, perda de controle.
	/// Ao atacar, Magias geralmente consomem Consciência e Vigor; Golpes físicos geralmente consomem Vigor.
	/// </summary>
	[Range(0, 100)]
	public float Vigor = 100, Vida = 100, Consciencia = 100;
	public bool Consciente = true, Físico = true;
	private Vector3 posicaoCabeca;

	private void Start()
	{
		Canvas = GameObject.Find("Canvas").transform;
		Camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
		posicaoCabeca = new Vector3(0,2,0);
		Anim = gameObject.GetComponent<ControladorAnimator>();
	}

	private Dano ReduzirDano(Dano dano)
	{
		dano.Fisico -= Mathf.Clamp(Random.Range(0, 10), 0, (int)dano.Fisico);
		dano.Psicologico -= Mathf.Clamp(Random.Range(0, 10), 0, (int)dano.Psicologico);
		dano.Vital -= Mathf.Clamp(Random.Range(0, 10), 0, (int)dano.Vital);
		return dano;
	}

	public void ExibirDano(Dano dano)
	{
		DanoTexto instancia = null;
		int danoInt = 0;
		Vector2 posicao = Camera.WorldToScreenPoint(gameObject.transform.position + posicaoCabeca);
		if (dano.Vital != null)
			danoInt = (int) dano.Vital;
		else if (dano.Fisico != null)
			danoInt = (int)dano.Fisico;
		else if(dano.Psicologico != null)
			danoInt = (int)dano.Psicologico;
		
		instancia = (danoInt > 5) ? Instantiate(PrefabCritico) : Instantiate(PrefabNormal);
		instancia.transform.SetParent(Canvas, false);
		instancia.transform.position = posicao + new Vector2(Random.Range(-10, 10), Random.Range(0, 10));
		instancia.InformarDano(danoInt);
	}

	public void ReceberDano(Dano dano)
	{
		dano = ReduzirDano(dano);
		Vigor -= (int)dano.Fisico;
		Vida -= (int)dano.Vital;
		Consciencia -= (int)dano.Psicologico;
		ExibirDano(dano);
		Anim.Atingido = true;
	}

	public void EncerrarDano()
	{
		Anim.Atingido = false;
	}
}
