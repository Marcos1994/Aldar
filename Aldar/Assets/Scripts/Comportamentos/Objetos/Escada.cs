using UnityEngine;
using System.Collections;

/// <summary>
/// Esse script é responsavem por setar e remover os objetos de EscadaProxima de personagens.
/// Os objetos com esse script deverão possuir dois colliders:
/// -um com sua altura, largura e espessura, sendo esta 1.5 maior para cada lado;
/// -outro com função trigger ativada 0.5 de espessura, 2 a menos de altura do objeto principal e a 1.5 de distancia dele
/// </summary>
public class Escada : MonoBehaviour
{
	void OnTriggerEnter(Collider ator)
	{
		if (ator.gameObject.layer == LayerMask.NameToLayer("Personagens"))
			ator.gameObject.GetComponent<PersonagemMovel>().EscadaProxima = gameObject.transform;
	}

	void OnTriggerExit(Collider ator)
	{
		if (ator.gameObject.layer == LayerMask.NameToLayer("Personagens"))
			ator.gameObject.GetComponent<PersonagemMovel>().EscadaProxima = null;
	}
}
