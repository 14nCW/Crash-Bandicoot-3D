using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameHandler : MonoBehaviour {
    [SerializeField] private CinemachineFreeLook cinemachine;
    [SerializeField] TextMeshProUGUI boxLeft;
    [SerializeField] TextMeshProUGUI winAnnounce;
    float inactiveTime = 3f;
    float desiredTime = 2f;
    float elapsedTime = 0;
    int boxesOnMap;

    void Awake() {
        boxesOnMap = GameObject.FindGameObjectsWithTag("Box").Length;
        boxLeft.text = "Boxes Left: " + boxesOnMap;
    }

    void Update() {
        CameraDistance();
    }

    public void RemovedBox() {
        if (boxesOnMap <= 1) {
            StartCoroutine(ReloadMap());
            boxLeft.text = "Boxes Left: 0";
            
        } else {
            boxesOnMap -= 1;
            boxLeft.text = "Boxes Left: " + boxesOnMap;
        }
    }

    private void CameraDistance() {
        if (Player.isMoving) {
            cinemachine.m_Orbits[1].m_Radius = 7f;
            inactiveTime = 3f;
            elapsedTime = 0f;
        } else {
            inactiveTime -= Time.deltaTime;
            if (inactiveTime <= 0) {
                elapsedTime += Time.deltaTime;
                float persentageComplete = elapsedTime / desiredTime;
                inactiveTime = 0f;
                cinemachine.m_Orbits[1].m_Radius = Mathf.Lerp(7f, 15f, persentageComplete);
            }
        }
    }

    IEnumerator ReloadMap() {
        winAnnounce.text = "Gratulacje";
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("MainScene");
    }
}
