using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class DeathBarrier : MonoBehaviour
{
    
    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            
            GameManager.Instance.ResetLevel(3f);
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
