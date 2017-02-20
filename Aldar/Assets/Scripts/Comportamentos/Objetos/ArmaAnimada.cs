using UnityEngine;
using System.Collections;

/// <summary>
/// Esse script é responsavem por ativar e desativas as animações contidas no animator de uma arma.
/// </summary>
public class ArmaAnimada : Arma
{
	private Animator Anim;

	void Start ()
	{
		Anim = gameObject.GetComponent<Animator>();
		Anim.SetBool("Equipada", false);
	}

	public override void Equipar(Transform local, bool equipar = true)
	{
		base.Equipar(local, equipar);
		Anim.SetBool("Equipada", equipar);
	}
}
