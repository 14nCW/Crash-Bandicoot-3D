using System.Collections;
using UnityEngine;

public class Box : MonoBehaviour {
    [SerializeField] LayerMask playerLayer;
    [SerializeField] [Range(-0.5f, 0.9f)] float hitDistance;
    private bool isPlayerOnTop = false;
    GameHandler gameHandler;
    private GameObject player;
    Vector3 recoil;

    void Start() {
        gameHandler = GameObject.FindGameObjectWithTag("Handler").GetComponent<GameHandler>();
        player = GameObject.Find("Player");
    }

    void Update() {
        if (isPlayerOnTop == false) {
            PlayerOnTop();
        }
    }

    private void PlayerOnTop() {
        RaycastHit raycastHit;
        bool isHit = Physics.BoxCast(transform.position, transform.lossyScale / 2, transform.up, out raycastHit,
            transform.rotation, hitDistance);
        if (isHit) {
            DestroyBox();
            isPlayerOnTop = true;
        }
    }

    private void DestroyBox() {
        PlayerRecoil();
        Destroy(gameObject);
        gameHandler.RemovedBox();
    }

    private void PlayerRecoil() {
        player.GetComponent<Player>().Jump(5f);
    }
}
