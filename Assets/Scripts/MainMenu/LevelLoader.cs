using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public float transmissionTime = 1f;

    public void LoadLevel(int levelNumber) 
    {
        StartCoroutine(PlayAnimationAndLoad(levelNumber));
    }

    IEnumerator PlayAnimationAndLoad(int levelNumber) 
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(transmissionTime);
        SceneManager.LoadScene("Level" + levelNumber);
    }
}
