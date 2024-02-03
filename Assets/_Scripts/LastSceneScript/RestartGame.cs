using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    [SerializeField] private Button button;
    private void Start()
    {
        button.onClick.AddListener(() => {
            SoundManager.Instance.Play(Sounds.ButtonClick);
            SceneManager.LoadScene(0); });
    }
}
