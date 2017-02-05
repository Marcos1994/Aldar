using UnityEngine;
using System.Collections;

public class Bastao : PosturaCombate
{
	public override void Iniciar()
	{
		Object.Instantiate(Personagem.armas[0], Personagem.slots[(int)EnumSlots.CostasDireita]);
		Transform arma = Personagem.slots[(int)EnumSlots.CostasDireita].GetChild(0);
		arma.position = arma.parent.position;
		arma.rotation = arma.parent.rotation;
	}

	public override void EmpunharArma(bool armando)
	{
		Arma arma;
		if (armando)
		{
			arma = Personagem.slots[(int)EnumSlots.CostasDireita].GetComponentInChildren<Arma>();
			arma.Equipar(Personagem.maos[(int)EnumMaos.Direita]);
		}
		else
		{
			arma = Personagem.maos[(int)EnumMaos.Direita].GetComponentInChildren<Arma>();
			arma.Equipar(Personagem.slots[(int)EnumSlots.CostasDireita], false);
		}
	}

	public override float FinalizarAtaque(int ataque)
	{
		switch(ataque)
		{
			case 1:
				return PrimeiroAtaque();
			case 2:
				return SegundoAtaque();
			case 3:
				return TerceiroAtaque();
		}
		return 0.3F;
	}

	private float PrimeiroAtaque()
	{
		return 0.5F;
	}

	private float SegundoAtaque()
	{
		return 0.5F;
	}

	private float TerceiroAtaque()
	{
		return 0.2F;
	}
}
