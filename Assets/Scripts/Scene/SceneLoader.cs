using UnityEngine;
using UnityEngine.SceneManagement;
public sealed class SceneLoader : MonoBehaviour
{

    [SerializeField]
    private string ServerScene1Name;

    [SerializeField]
    private string ClienceScene1Name;


    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform is RuntimePlatform.WindowsServer or RuntimePlatform.OSXServer or RuntimePlatform.LinuxEditor)
        {
            SceneManager.LoadScene(ServerScene1Name);
        }
        else
        {
            SceneManager.LoadScene(ClienceScene1Name);
        }
    }

    
}
