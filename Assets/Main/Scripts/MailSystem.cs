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
        ReceiveMail("HR", "Welcome", "Whelcome to the company!");
        ReceiveMail("CEO", "Project Update", "Check smth.");
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

        var btnObj = Instantiate(mailButtonPrefab, mailListParent);
        var btnText = btnObj.GetComponentInChildren<TMP_Text>();
        btnText.text = inbox[index].subject;

        int i = index;
        btnObj.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => DisplayMail(i));
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
