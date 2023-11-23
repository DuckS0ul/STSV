using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SelectionManager : MonoBehaviour
{
    public static SelectionManager Instance { get; set; }


    public bool onTarget;

    public GameObject selectedObject;

    public GameObject InteractionInfo;
    TextMeshProUGUI interaction_text;

    public Image centerDotImage;
    public Image handIcon;


    private void Start()
    {
        onTarget = false;
        interaction_text = InteractionInfo.GetComponent<TextMeshProUGUI>();
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();

            if (interactable && interactable.playerInRange)
            {
                onTarget = true;
                selectedObject = interactable.gameObject;
                InteractionInfo.SetActive(true);
                interaction_text.text = interactable.GetItemName();

                if (interactable.CompareTag("pickable"))
                {
                    centerDotImage.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                }
                else
                {
                    
                    handIcon.gameObject.SetActive(false);
                    centerDotImage.gameObject.SetActive(true);
                }


            }
            else
            {
                onTarget = false;
                InteractionInfo.SetActive(false);
                handIcon.gameObject.SetActive(false);
                centerDotImage.gameObject.SetActive(true);
            }

        }
        else
        {
            onTarget = false;
            InteractionInfo.SetActive(false);
            handIcon.gameObject.SetActive(false);
            centerDotImage.gameObject.SetActive(true);
        }
    }
}


