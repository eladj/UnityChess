using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour {

	public Tile currentTile;
	public ColorType color;
	public bool visible = true;

	public enum ColorType {White, Black};

	public abstract bool IsMoveLegal(Tile targetTile);

	void Start () {
		// Get the position of the tile
//		 transform.localPosition = currentTile.transform.localPosition;
		transform.position = currentTile.transform.position;
	}
}

