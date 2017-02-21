using UnityEngine;
using System.Collections;

public class Bastao : PosturaCombate
{
	private BoxCollider box;

	public override void Iniciar()
	{
		Object.Instantiate(Personagem.Armas[0], Personagem.Slots[(int)EnumSlots.CostasDireita]);
		Transform arma = Personagem.Slots[(int)EnumSlots.CostasDireita].GetChild(0);
		arma.position = arma.parent.position;
		arma.rotation = arma.parent.rotation;
	}

	private void AtivarColisor(Vector3 posicao)
	{
		BoxCollider box = Personagem.gameObject.AddComponent<BoxCollider>();
		box.center = posicao;
		box.isTrigger = true;
	}

	public override void EmpunharArma(bool armando)
	{
		Arma arma;
		if (armando)
		{
			arma = Personagem.Slots[(int)EnumSlots.CostasDireita].GetComponentInChildren<Arma>();
			arma.Equipar(Personagem.Maos[(int)EnumMaos.Direita]);
		}
		else
		{
			arma = Personagem.Maos[(int)EnumMaos.Direita].GetComponentInChildren<Arma>();
			arma.Equipar(Personagem.Slots[(int)EnumSlots.CostasDireita], false);
		}
	}

	public override void IniciarAtaque(int ataque)
	{
		this.ataque = ataque;
		Arma arma = Personagem.Maos[(int)EnumMaos.Direita].GetComponentInChildren<Arma>();
		float dano = 0;
		switch (ataque)
		{
			case 1:
				AtivarColisor(new Vector3(0, 1, 1));
				dano = arma.DanoBase - 3;
				break;
			case 2:
				AtivarColisor(new Vector3(0, 1, 1));
				dano = arma.DanoBase - 1;
				break;
			case 3:
				AtivarColisor(new Vector3(0, 1, 1.2F));
				dano = arma.DanoBase * 1.2F;
				break;
		}
	}

	public override float FinalizarAtaque()
	{
		Arma arma = Personagem.Maos[(int)EnumMaos.Direita].GetComponentInChildren<Arma>();
		switch(ataque)
		{
			case 1:
				return 0.2F;
			case 2:
				return 0.2F;
			case 3:
				return 0.2F;
		}
		return 0.3F;
	}
}
