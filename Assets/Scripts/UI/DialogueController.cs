using Player;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject _holdPanel;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private TextMeshProUGUI _nameText;

    [SerializeField] private Image _speakerImage;

    [SerializeField] private GameObject _continueButton;

    [SerializeField] private List<Dialogue> _dialogues;

    [SerializeField] private float timeBtwnChars = 0.2f;
    //[SerializeField] private float timeBtwnWords = 1f;

    private int i = 0;

    [SerializeField] private UnityEvent _function;


    private void Start()
    {
        EndCheck();
    }

    public void EndCheck()
    {
        if (i <= _dialogues.Count - 1)
        {
            PlayerController.isStop = true;
            _dialogueText.text = _dialogues[i].dialogue;
            _nameText.text = _dialogues[i].speakerName;
            _speakerImage.sprite = _dialogues[i].faceImage;
            StartCoroutine(TextVisible());
        }
        else
        {
            PlayerController.isStop = false;
            _holdPanel.SetActive(false);
            if (_function != null)
                _function.Invoke();
        }
    }
    private IEnumerator TextVisible()
    {
        _dialogueText.ForceMeshUpdate();
        int totalVisibleCharacters = _dialogueText.textInfo.characterCount;
        int counter = 0;

        _continueButton.SetActive(false);

        while (true)
        {
            int visibleCount = counter % (totalVisibleCharacters + 1);
            _dialogueText.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalVisibleCharacters)
            {
                _continueButton.SetActive(true);
                i += 1;
                break;
            }

            counter++;
            yield return new WaitForSeconds(timeBtwnChars);
        }
    }

    public void Continue()
    {
        EndCheck();
    }

}
