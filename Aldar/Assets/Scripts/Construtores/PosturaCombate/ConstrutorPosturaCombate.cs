using UnityEngine;
using System.Collections;

public static class ConstrutorPosturaCombate
{
	public static PosturaCombate Construir(PersonagemCombate personagem, EnumTipoArmamento tipoArmamento)
	{
		PosturaCombate postura = null;
        switch (tipoArmamento)
		{
			case EnumTipoArmamento.Bastao:
				postura = new Bastao();
				break;
		}
		postura.Personagem = personagem;
		return postura;
	}
}
