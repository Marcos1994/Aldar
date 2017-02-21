using UnityEngine;
using System.Collections;

public abstract class PosturaCombate
{
	protected int ataque;

	public PersonagemCombate Personagem { get; set; }
	public abstract void Iniciar();
	public abstract void EmpunharArma(bool armando);
	public abstract void IniciarAtaque(int ataque);
	public abstract float FinalizarAtaque();
}
