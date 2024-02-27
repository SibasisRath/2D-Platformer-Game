using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static void LevelSelector(AllLevels level)
    {

        LevelStates levelStates = LevelManager.Instance.GetLevelStates(level);
        switch (levelStates)
        {
            case LevelStates.Completed:
            case LevelStates.Unlocked:
                SoundManager.Instance.Play(Sounds.ButtonClick);
                SceneManager.LoadScene((int)level);
                break;
            case LevelStates.Locked:
                SoundManager.Instance.Play(Sounds.LevelLocked);
                break;
            default:
                break;
        }
       
    }
}
