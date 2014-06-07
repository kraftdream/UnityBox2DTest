using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeGenerator : MonoBehaviour {

	private const int MAX_ROPE_LENGTH = 20;
	private const int MIN_ROPE_LENGTH = 1;
	private const string ROPE_PIECE_OBJECT_NAME = "ROPE_PIECE";
	private const string ROPE_NAME = "GEN_ROPE";

	public Sprite ropePieceSprite;
	public GameObject fromObject;
	public GameObject toObject;

	private List<GameObject> mRopePiecesList;

	private GameObject mRopeMainObject;
	private GameObject ropePieceObject;

	private SpriteRenderer ropePieceSpriteRenderer;
	private Rigidbody2D ropePieceRigidbody;
	private DistanceJoint2D ropePieceJoint;

	private float ropePieceCenterX;
	private float ropePieceCenterY;

	// Use this for initialization
	void Start () {
		initVariables ();
		genRope ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void initVariables() {
		ropePieceCenterX = ropePieceSprite.bounds.center.normalized.x;
		ropePieceCenterY = ropePieceSprite.bounds.center.normalized.y;
		mRopePiecesList = new List<GameObject> ();
		ropePieceObject = new GameObject ();

		mRopeMainObject = new GameObject (ROPE_NAME);
	}

	private GameObject initRopePiece() {
		GameObject ropePiece = new GameObject (ROPE_PIECE_OBJECT_NAME);

		if (ropePieceSprite != null) {
			ropePieceSpriteRenderer = ropePiece.AddComponent<SpriteRenderer>();
			ropePieceRigidbody = ropePiece.AddComponent<Rigidbody2D>();
			ropePieceJoint = ropePiece.AddComponent<DistanceJoint2D>();

			ropePieceSpriteRenderer.sprite = ropePieceSprite;

			ropePieceRigidbody.mass = 1f;
			//ropePieceRigidbody.gravityScale = 0.1f;

			ropePieceJoint.distance = 0.1f;
			ropePieceJoint.maxDistanceOnly = true;
			ropePieceJoint.anchor = new Vector2(0, ropePieceCenterY);
		} else
			Debug.Log ("Please add sprite object to script (it's necessary)");

		return ropePiece;
	}

	private void genRope() {
		for (int i = 0; i < MAX_ROPE_LENGTH; i++) {
			ropePieceObject = initRopePiece();
			ropePieceObject.transform.parent = mRopeMainObject.transform;

			if (mRopePiecesList.Count > 0) {
				ropePieceJoint.connectedBody = mRopePiecesList[mRopePiecesList.Count - 1].rigidbody2D;
			}
			else
				ropePieceJoint.connectedBody = fromObject.rigidbody2D;

			mRopePiecesList.Add(ropePieceObject);
		}

		toObject.GetComponent<DistanceJoint2D> ().connectedBody = ropePieceObject.rigidbody2D;
	}
}
