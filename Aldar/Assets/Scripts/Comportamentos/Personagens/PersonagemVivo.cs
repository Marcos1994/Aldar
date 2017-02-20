using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonagemVivo : MonoBehaviour
{
	/// <summary>
	/// Dano reduz Vida e pode reduzir Vigor (dano físico) e/ou Consciência (dano psicológico).
	/// Vida = 0, morto; Vigor = 0, mais lento e menos preciso; Consciencia = 0, perda de controle.
	/// Ao atacar, Magias geralmente consomem Consciência e Vigor; Golpes físicos geralmente consomem Vigor.
	/// </summary>
	[Range(0, 100)]
	public float Vigor = 100, Vida = 100, Consciencia = 100;
	public bool Consciente = true, Físico = true;


}
