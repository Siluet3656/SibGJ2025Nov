using System;
using TMPro;
using UnityEngine;

namespace Main.Scripts
{
    public class UnreadMailCounter : MonoBehaviour
    {
        [SerializeField] private GameObject _unreadHolder;
        [SerializeField] private TMP_Text _unreadText;
        
        private int _unread;

        private void Update()
        {
            _unread = 0;
            
            foreach (var letter in G.MailSystem.inbox)
            {
                if (letter.isRead == false)
                {
                    _unread++;
                }
            }

            if (_unread > 0)
            {
                _unreadHolder.SetActive(true);
                _unreadText.text = _unread.ToString();
            }
            else
            {
                _unreadHolder.SetActive(false);
            }
        }
    }
}
