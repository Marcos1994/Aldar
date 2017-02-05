using UnityEngine;
using System.Collections;

/// <summary>
/// Toda arma de personagem deverá conter esse scrpt para ser equipada.
/// </summary>
public class Arma : MonoBehaviour
{
	public virtual void Equipar(Transform local, bool equipar = true)
	{
		gameObject.transform.parent = local;
		gameObject.transform.position = local.position;
		gameObject.transform.rotation = local.rotation;
	}
}
