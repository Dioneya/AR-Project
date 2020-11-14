using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public static void OpenScenLoader(int SceneNmb)
    {
        SceneManager.LoadScene(2); // Сцена загрузки
        SceneLoader.SetSceneNmb(SceneNmb);
    }
    public void LoadAsyncScene(int sceneIndex)
    {
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
