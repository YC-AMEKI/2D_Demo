using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TalkSystem : MonoBehaviour
{

    [Header("UI")]
    [SerializeField] private TMP_Text sentenceLabel;
    [SerializeField] private TMP_Text nameLabel;
    [SerializeField] private TMP_Text continueText;

    //[SerializeField] private Image faceImage;
    [SerializeField, Range(0, 1)] private float textSpeed;

    [Header("文本文件")]
    [SerializeField] private TextAsset textFile;
    public int index;

    List<string> textList = new List<string>();
    private bool textFinished;

    private void Awake()
    {
        GetTextFromFile(textFile);
    }

    private void OnEnable()
    {
        //continueText.gameObject.SetActive(false);
        textFinished = true;
        string[] text = textList[index].Split(":");
        nameLabel.text = text[0];
        sentenceLabel.text = text[1];
        index++;
        continueText.gameObject.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        
        //index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //if (inputControl.Player.talk.WasPressedThisFrame()) DialogShowText();
    }

    private void GetTextFromFile(TextAsset file)
    {
        textList.Clear();
        index = 0;

        foreach (var line in file.text.Split('\n'))
        {
            textList.Add(line);
        }
    }

    private void OnDisable()
    {
        index = 0;
    }

    private void DialogShowText()
    {
        continueText.gameObject.SetActive(false);
        if (index < textList.Count && textFinished)
            StartCoroutine(SetTextUI());
        if (index == textList.Count)
        {
            gameObject.SetActive(false);
            index = 0;
            return;
        }
    }

    IEnumerator SetTextUI()
    {
        sentenceLabel.text = "";
        textFinished = false;

        string[] text = textList[index].Split(":");
        nameLabel.text = text[0];

        for (int i = 0; i < text[1].Length; i++) 
        { 
            sentenceLabel.text += text[1][i];
            yield return new WaitForSeconds(textSpeed);
        }
        
        textFinished = true;
        continueText.gameObject.SetActive(true);
        index++;
    }
}
