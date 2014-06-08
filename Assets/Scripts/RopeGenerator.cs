using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RopeGenerator : MonoBehaviour {
	private const string ROPE_PIECE_OBJECT_NAME = "ROPE_PIECE";
	private const string ROPE_NAME = "GEN_ROPE";

	public Sprite ropePieceSprite;
	public GameObject fromObject;
	public GameObject toObject;

	[Range(1, 100)]
	public int maxRopePieces;

	[Range(1, 10)]
	public float pieceMass;

	[Range(1, 10)]
	public float pieceGravityScale;

	[Range(1, 10)]
	public float pieceDrag;

	public bool distanseTruth;

	private List<GameObject> mRopePiecesList;

	private GameObject mRopeMainObject;
	private GameObject ropePieceObject;

	private SpriteRenderer ropePieceSpriteRenderer;
	private Rigidbody2D ropePieceRigidbody;
	private HingeJoint2D ropePieceJoint;

	private float ropePieceCenterX;
	private float ropePieceCenterY;

	private float ropePieceWidth;
	private float ropePieceHeight;

	private float distanceBetweenObjects;

	// Use this for initialization
	void Start () {
		initVariables ();
		genRope ();
	}

	// Update is called once per frame
	void Update () {
		
	}

	private void initVariables() {
		ropePieceCenterX = ropePieceSprite.bounds.center.x;
		ropePieceCenterY = ropePieceSprite.bounds.center.y;

		ropePieceWidth = ropePieceSprite.bounds.size.x;
		ropePieceHeight = ropePieceSprite.bounds.size.y;

		mRopePiecesList = new List<GameObject> ();

		mRopeMainObject = new GameObject (ROPE_NAME);

		//get the distance between gameobject
		if (fromObject != null && toObject != null)
			distanceBetweenObjects = Vector3.Distance (fromObject.transform.position, toObject.transform.position);
	}

	private GameObject initRopePiece() {
		GameObject ropePiece = new GameObject (ROPE_PIECE_OBJECT_NAME);

		if (ropePieceSprite != null) {
			ropePieceSpriteRenderer = ropePiece.AddComponent<SpriteRenderer>();
			ropePieceRigidbody = ropePiece.AddComponent<Rigidbody2D>();
			ropePieceJoint = ropePiece.AddComponent<HingeJoint2D>();

			ropePieceSpriteRenderer.sprite = ropePieceSprite;

			ropePieceRigidbody.mass = pieceMass;
			ropePieceRigidbody.gravityScale = pieceGravityScale;
			ropePieceRigidbody.angularDrag = pieceDrag;

			ropePieceJoint.anchor = new Vector2(0, (ropePieceHeight / 2) + ropePieceCenterY);
		} else
			Debug.Log ("Please add sprite object to script (it's necessary)");

		return ropePiece;
	}

	private void genRope() {
		for (int i = 0; i < maxRopePieces; i++) {
			ropePieceObject = initRopePiece();
			ropePieceObject.transform.parent = mRopeMainObject.transform;

			if (mRopePiecesList.Count > 0 ) {
				ropePieceJoint.connectedBody = mRopePiecesList[mRopePiecesList.Count - 1].rigidbody2D;
			}
			else if (fromObject != null)
				ropePieceJoint.connectedBody = fromObject.rigidbody2D;

			mRopePiecesList.Add(ropePieceObject);
		}

		if (distanseTruth) {
			if (toObject != null && distanceBetweenObjects < (ropePieceHeight * mRopePiecesList.Count))
				toObject.GetComponent<HingeJoint2D> ().connectedBody = ropePieceObject.rigidbody2D;
		}
		else if (toObject != null)
			toObject.GetComponent<HingeJoint2D> ().connectedBody = ropePieceObject.rigidbody2D;
	}
}
