using UnityEngine;
using UnityEngine.SceneManagement;
public class retry : MonoBehaviour
{
    public void Retry()
    {
        SceneManager.LoadSceneAsync(0);
    }

}
