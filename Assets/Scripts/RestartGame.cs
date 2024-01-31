using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(() => {
            SoundManager.Instance.Play(Sounds.ButtonClick);
            SceneManager.LoadScene(0); });
    }
}
