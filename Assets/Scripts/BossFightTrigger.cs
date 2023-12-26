using Cinemachine;
using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _followCam;
    [SerializeField] private Animator _cageAnimator;
    [SerializeField] private Transform _fightCamTarget;
    [SerializeField] private GameObject _diaglogueCanvasPanel;
    [SerializeField] private GameObject _dialogueController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController.isStop = true;
            _cageAnimator.SetTrigger("Close");
            _followCam.m_Lens.OrthographicSize = 6.5f;
            _followCam.Follow = _fightCamTarget;
            GetComponent<Collider2D>().enabled = false;
            _diaglogueCanvasPanel.SetActive(true);
            _dialogueController.SetActive(true);
        }
    }
}
