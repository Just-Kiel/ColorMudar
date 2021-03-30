using UnityEngine;
using UnityEngine.EventSystems;

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

        public void Update()
        {
            
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
