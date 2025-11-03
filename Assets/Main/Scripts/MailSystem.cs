using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MailSystem : MonoBehaviour
{
    [Serializable]
    public class MailMessage
    {
        public string sender;
        public string subject;
        [TextArea(3, 10)] public string body;
        public DateTime receivedTime;
        public bool isRead;

        public MailMessage(string sender, string subject, string body)
        {
            this.sender = sender;
            this.subject = subject;
            this.body = body;
            this.receivedTime = DateTime.Now;
            this.isRead = false;
        }
    }

    [Header("UI References")]
    public TMP_Text senderText;      // Текстовое поле для имени отправителя
    public TMP_Text messageText;     // Основное поле для письма
    public TMP_Text subjectText;     // (опционально) Тема письма
    public Transform mailListParent; // Родитель для кнопок писем
    public GameObject mailButtonPrefab; // Префаб кнопки письма

    [Header("Data")]
    public List<MailMessage> inbox = new List<MailMessage>();

    public event Action<MailMessage> OnMailReceived;

    private void Awake()
    {
        G.MailSystem = this;
    }

    void Start()
    {
        // Для теста — добавим пару писем
        G.MailSystem.ReceiveMail(
            "HR Department",
            "Welcome",
            "Welcome to Щ Market, valued employee!\nWe're thrilled to have you join our growing family of dedicated workers. Your productivity defines your worth, and your worth defines our success.\n" +
            "Remember: every click matters!"
        );

        G.MailSystem.ReceiveMail(
            "CEO",
            "Project Update",
            "Welcome aboard.\n\nYour workstation is now linked to the main productivity stream. " +
            "Click the terminal to generate units. Units generate value. Value ensures the stability of your employment.\n" +
            "Keep your metrics positive. The system observes everything."
        );
    }

    public void ReceiveMail(string sender, string subject, string body)
    {
        MailMessage newMail = new MailMessage(sender, subject, body);
        inbox.Add(newMail);
        OnMailReceived?.Invoke(newMail);

        CreateMailButton(inbox.Count - 1);
        //Debug.Log($"📬 Новое письмо от {sender}: {subject}");
    }

    void CreateMailButton(int index)
    {
        if (mailButtonPrefab == null || mailListParent == null) return;

        var mail = inbox[index];
        var btnObj = Instantiate(mailButtonPrefab, mailListParent);
        var btnText = btnObj.GetComponentInChildren<TMP_Text>();
        btnText.text = mail.subject;

        // ✅ Настраиваем визуал в зависимости от прочитанности
        if (mail.isRead)
        {
            btnText.color = Color.white;
            btnText.fontStyle = FontStyles.Normal;
        }
        else
        {
            btnText.color = new Color(0.7f, 0, 0f); // тёмно-серый
            btnText.fontStyle = FontStyles.Bold;
        }

        int i = index;
        btnObj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            DisplayMail(i);
            UpdateMailButtonVisual(btnObj, inbox[i]);
        });
    }

    void UpdateMailButtonVisual(GameObject button, MailMessage mail)
    {
        var txt = button.GetComponentInChildren<TMP_Text>();
        txt.color = Color.black;
        txt.fontStyle = FontStyles.Normal;
    }


    public void DisplayMail(int index)
    {
        if (index < 0 || index >= inbox.Count) return;

        var mail = inbox[index];
        mail.isRead = true;

        senderText.text = $"From: {mail.sender}";
        subjectText.text = mail.subject;
        messageText.text = mail.body;
    }
}
