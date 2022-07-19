using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float loadWaitTime = 2f;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadLevel()
    {
        PlayerController.instance.canMove = false;
        UIController.instance.FadeToBlack();
        yield return new WaitForSeconds(loadWaitTime);
        SceneManager.LoadScene(1);
    }
}
