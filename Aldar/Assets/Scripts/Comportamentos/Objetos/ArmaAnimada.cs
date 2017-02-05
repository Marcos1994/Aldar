using UnityEngine;
using System.Collections;

public class ArmaAnimada : Arma
{
	private Animator anim;

	void Start ()
	{
		anim = gameObject.GetComponent<Animator>();
		anim.SetBool("Equipada", false);
	}

	public override void Equipar(Transform local, bool equipar = true)
	{
		base.Equipar(local, equipar);
		anim.SetBool("Equipada", equipar);
	}
}
