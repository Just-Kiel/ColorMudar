using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.Plugins.ButtonSoundsEditor
{
    public class ButtonClickSound : MonoBehaviour//,IPointerClickHandler
    {
        public AudioSource AudioSource;
        public AudioClip ClickSound;

        [SerializeField] GameObject currentButton = null;

        /*public void OnPointerClick(PointerEventData eventData)
        {
            PlayClickSound();
        }*/

        public void Start()
        {
            
        }

        public void Update()
        {
            AudioSource = GameObject.Find("AudioManager").GetComponent<AudioSource>();
            if (EventSystem.current.currentSelectedGameObject && (currentButton == null || currentButton.name != EventSystem.current.currentSelectedGameObject.name))
            {
                PlayClickSound();
                currentButton = EventSystem.current.currentSelectedGameObject;

            }
        }

        private void PlayClickSound()
        {
            AudioSource.PlayOneShot(ClickSound);
        }
    }

}
