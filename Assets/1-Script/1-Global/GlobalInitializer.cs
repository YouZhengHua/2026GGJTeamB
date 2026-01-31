using UnityEngine;

public class GlobalInitializer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    void Start()
    {

    }

}
