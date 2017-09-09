using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move {

	public TileLogic origin;
	public TileLogic destination;
	public PieceLogic piece;
	public Game.MoveType type;
	public Game.SideColor color;
	public bool PieceTaken = false;
	public Game.PieceType piecePromotedType;	// In case we promoted a pawn, what was the piece it promoted to
	public static int moveNumber = 0;

	public Move(TileLogic _origin, TileLogic _destination, bool addMoveNumber=true){
		origin = _origin;
		destination = _destination;
		piece = origin.GetPiece ();
		color = piece.color;

		// Check if this is a castle move
		if (piece.type == Game.PieceType.King) {
			if ((destination.column - origin.column) == 2) {
				type = Game.MoveType.CastleShort;
			} else if ((destination.column - origin.column) == -2){
				type = Game.MoveType.CastleLong;
			}
		}

		if (addMoveNumber) {
			moveNumber++;
		}
	}

	// Get list of all tiles in route from tile origin to destination
	public List<TileLogic> GetRoute(){
		List<TileLogic> route = new List<TileLogic> ();

		// Special case of knight - we return empty route
		if (piece is Knight) {
			return route;
		}

		// Special case - move to the same tile
		if (origin.row == destination.row && origin.column == destination.column) {
			return route;
		}

		int rowDirection = destination.row - origin.row > 0 ? 1 : -1;
		int colDirection = destination.column - origin.column > 0 ? 1 : -1;

		// Along a row
		if (origin.row == destination.row) {
			for (int col = origin.column + colDirection; col*colDirection < destination.column*colDirection; col += colDirection) {
				route.Add (new TileLogic (origin.row, col));
			}
		}

		// Along a column
		else if (origin.column == destination.column) {
			for (int row = origin.row + rowDirection; row*rowDirection < destination.row*rowDirection; row += rowDirection) {
				route.Add (new TileLogic (row, origin.column));
			}
		}
		// Diagonal
		else {
			for (int row = origin.row + rowDirection, col = origin.column + colDirection;
				row*rowDirection < destination.row*rowDirection && col*colDirection < destination.column*colDirection;
				row += rowDirection, col += colDirection) {
				route.Add (new TileLogic (row, col));
			}
		}
		return route;
	}

	public override string ToString(){
		string s;
		switch (type){
		case Game.MoveType.CastleShort:
			s = "0-0";
			break;
		case Game.MoveType.CastleLong:
			s = "0-0-0";
			break;
		case Game.MoveType.Promote:
			s = destination.Name () + "=" + PieceLogic.NameChar (piecePromotedType);
			break;
		default:
			s = piece.NameChar () + origin.Name () + "-" + destination.Name ();
			break;
		}
		return s;
	}
}
