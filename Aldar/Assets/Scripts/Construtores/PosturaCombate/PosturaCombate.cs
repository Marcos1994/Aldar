using UnityEngine;
using System.Collections;

public abstract class PosturaCombate
{
	public PersonagemCombate Personagem { get; set; }
	public abstract void Iniciar();
	public abstract void EmpunharArma(bool armando);
	public abstract float FinalizarAtaque(int ataque);
}
