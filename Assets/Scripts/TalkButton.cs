using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TalkButton : MonoBehaviour
{
    [SerializeField] private GameObject Button;
    [SerializeField] private GameObject talkUI;

    public bool talkButtonDown;
    public TalkSystem talkSystem;

    public PlayerInputControl inputControl;

    private void Awake()
    {
        inputControl = new PlayerInputControl();
    }

    private void OnEnable()
    {
        inputControl.Enable();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        //Button.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.activeSelf && inputControl.GameInput.talk.WasPressedThisFrame())
        {
            if (talkUI.activeSelf) talkSystem.SendMessage("DialogShowText");
            else talkUI.SetActive(true);
        }
        if (!Button.activeSelf)
        {
            talkUI.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Button.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button.SetActive(false);
        
    }

}
