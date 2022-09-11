using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupOnCollide : MonoBehaviour
{
    // Text box will have text logic (i.e. press button to go to next text, make choices), this component just activates it
    [SerializeField] private GameObject _textBox;

    // Start is called before the first frame update
    void Start()
    {
        _textBox.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D _)
    {
        _textBox.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D _)
    {
        _textBox.SetActive(false);
    }
}
