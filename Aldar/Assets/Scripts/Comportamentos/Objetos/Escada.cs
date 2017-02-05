using UnityEngine;
using System.Collections;

public class Escada : MonoBehaviour
{
	void OnTriggerEnter(Collider ator)
	{
		if (ator.gameObject.layer == LayerMask.NameToLayer("Personagens"))
			ator.gameObject.GetComponent<PersonagemMovel>().escadaProxima = gameObject.transform;
	}

	void OnTriggerExit(Collider ator)
	{
		if (ator.gameObject.layer == LayerMask.NameToLayer("Personagens"))
			ator.gameObject.GetComponent<PersonagemMovel>().escadaProxima = null;
	}
}
