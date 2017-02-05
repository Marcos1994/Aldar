using UnityEngine;
using System.Collections;

public class StatusPersonagem : MonoBehaviour
{
	/*-------------- Componentes e Controles ----------------*/
	private ControladorAnimator anim;
	public ControladorAnimator ControleAnim { get { return anim; } }

	protected void Start()
	{
		anim = new ControladorAnimator(GetComponent<Animator>());
	}
}
