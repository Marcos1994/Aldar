using System;
using UnityEngine;

public class ControleJogador : Controle
{
	private PersonagemMovel movimentacao;
	private PersonagemCombate combate;

	void Start()
    {
		//base.Start();
		movimentacao = GetComponent<PersonagemMovel>();
		combate = GetComponent<PersonagemCombate>();
	}


    void Update()
    {
		if (Input.GetAxis("Jump") != 0)
			movimentacao.Pular();
		movimentacao.Andar(Input.GetAxis("Fire3") > 0);
		if (Input.GetKeyDown(KeyCode.Q))
			movimentacao.EntrarEscada();

		if (Input.GetKeyDown(KeyCode.E))
			combate.TrocarPosturaCombate();
		if (Input.GetKey(KeyCode.A))
			combate.Atacar();
	}

	
    void FixedUpdate()
    {
		movimentacao.Mover(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
	}
}