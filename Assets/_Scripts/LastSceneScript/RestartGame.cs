using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    private void Start()
    {
        mainButton.onClick.AddListener(() => {
            SoundManager.Instance.Play(Sounds.ButtonClick);
            SceneManager.LoadScene(0); });
    }
}
