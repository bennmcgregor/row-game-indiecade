using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieCade;

public class PopupOnPress : MonoBehaviour
{
    public GameObject pressButtonToInteract;
    public GameObject textBox;
    public PlayerInputManager playerInputManager;
    private ObjectInteractionControl objectInteractionControl;

    bool colliding = false;

    // Start is called before the first frame update
    void Start()
    {
        pressButtonToInteract = GameObject.Find("Press Space To Interact Text");
        pressButtonToInteract.SetActive(false);

        colliding = false;

        textBox = transform.Find("Text Box").gameObject;
        textBox.SetActive(false);

        objectInteractionControl = GetComponent<ObjectInteractable>().objectInteractionControl;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        pressButtonToInteract.SetActive(true);
        colliding = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        pressButtonToInteract.SetActive(false);
        textBox.SetActive(false);
        colliding = false;
    }

    public void DeactivateText()
    {
        textBox.SetActive(false);
        pressButtonToInteract.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectInteractionControl.interacting && colliding)
        {
            pressButtonToInteract.SetActive(false);
            textBox.SetActive(true);
        }
    }
}
