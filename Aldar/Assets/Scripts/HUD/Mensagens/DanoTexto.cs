using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DanoTexto : MonoBehaviour
{
	public Animator Anim;
	private Text textComponent;

	private void Start()
	{
		AnimatorClipInfo[] infos = Anim.GetCurrentAnimatorClipInfo(0);
		Destroy(gameObject, infos[0].clip.length);
	}

	public void InformarDano(int dano)
	{
		textComponent = Anim.GetComponent<Text>();

		if (dano > 0)
			textComponent.text = dano.ToString();
		else
		{
			textComponent.text = "Erro";
			textComponent.color = Color.gray;
		}
	}
}
