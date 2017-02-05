using UnityEngine;
using System.Collections;

public class Arma : MonoBehaviour
{
	public virtual void Equipar(Transform local, bool equipar = true)
	{
		gameObject.transform.parent = local;
		gameObject.transform.position = local.position;
		gameObject.transform.rotation = local.rotation;
	}
}
