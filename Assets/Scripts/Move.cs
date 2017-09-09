using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {

	public TileLogic origin;
	public TileLogic destination;
	public PieceLogic piece;
	public bool PieceTaken = false;
	public static int moveNumber = 0;

	public Move(TileLogic _origin, TileLogic _destination){
		origin = _origin;
		destination = _destination;
		piece = origin.GetPiece ();
		moveNumber++;
	}

	public override string ToString(){
		return piece.NameChar() + origin.Name () + "-" + destination.Name ();
	}
}
