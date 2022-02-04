using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace UIScripts
{
    public class NameInputHandler : MonoBehaviour
    {
        //Connected Menus
        [SerializeField] private MenuUIHandler uiHandler;
        [SerializeField] private GameObject StartMenu;
        
        [SerializeField] private TMP_InputField nameInput;
        [SerializeField] private GameObject enterButton;
        [SerializeField] private FadeInFadeOut errorText;
        private Button _button;
        private Animator _buttonAnimator;

        //Animation hashes
        private int _pressed;
        private int _selected;

        private void Awake()
        {
            _button = enterButton.GetComponent<Button>();
            _buttonAnimator = enterButton.GetComponent<Animator>();
            // Faster than using string in update, not that it matters much on a button.
            _pressed = Animator.StringToHash("Pressed");
            _selected = Animator.StringToHash("Selected");
            
        }

        private void Update()
        {
            // Activate our submit button if Enter is pressed and play animation.
            if (Input.GetKeyDown(KeyCode.Return))
            {
                _buttonAnimator.SetTrigger(_pressed);
                _buttonAnimator.SetTrigger (_selected);
                _button.onClick.Invoke();
            }
            
        }
        
        public void SetEnteredName()
        {
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(null);
            
            // If no text is entered, do not submit
            if (string.IsNullOrWhiteSpace(nameInput.text))
            {
                errorText.StartFadeInFadeOut();
                return;
            }

            PlayerManager.Instance.setPlayerData(nameInput.text);
            uiHandler.ChangeMenu(gameObject, StartMenu);
        }
    }
}
