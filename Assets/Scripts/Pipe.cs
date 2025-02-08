using System.Collections;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Transform connection;
    public KeyCode enterKeyCode = KeyCode.S;
    public Vector3 enterDirection = Vector3.down; 
    public Vector3 exitDirection = Vector3.zero; 

    private void OnTriggerStay2D (Collider2D other)
    {
        if (connection != null && other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(enterKeyCode))
            {
                StartCoroutine(Enter(other.transform));
            }
        }
    }
    private IEnumerator Enter(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;

        Vector3 enterPosition = transform.position + enterDirection;
        Vector3 enterScale = Vector3.one * 0.5f; 

        yield return Move(player, enterPosition, enterScale);
        yield return new WaitForSeconds(1f); 
        //camera sẽ thay đổi chế độ (SetUnderground) tùy thuộc vào vị trí kết nối (connection.position).
        bool underground = connection.position.y < 0f;
        Camera.main.GetComponent<SideScrolling>().SetUnderground(underground);

        if (exitDirection != Vector3.zero)
        {
            //người chơi sẽ ra khỏi ống với hướng được chỉ định, nếu không, họ sẽ đứng tại vị trí kết nối mà không thay đổi hướng.
            player.position = connection.position - exitDirection;
            yield return Move(player, connection.position + exitDirection, Vector3.one);
        }
        else 
        {
            player.position = connection.position;
            player.localScale = Vector3.one;
        }

        player.GetComponent<PlayerMovement>().enabled = true;
    }

    private IEnumerator Move(Transform player, Vector3 endPosition, Vector3 endScale)  
    {
        float elapsed = 0f;
        float duration = 1f;

        Vector3 startPosition = player.position;
        Vector3 startScale = player.localScale;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            player.position = Vector3.Lerp(startPosition, endPosition, t);
            player.localScale = Vector3.Lerp(startScale, endScale,t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        player.position = endPosition;
        player.localScale = endScale;

        
    }
}
