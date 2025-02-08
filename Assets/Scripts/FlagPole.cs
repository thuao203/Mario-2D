using System.Collections;
using UnityEngine;

public class FlagPole : MonoBehaviour
{
    public Transform flag;
    public Transform poleBottom;
    public Transform castle;
    public float speed = 6f;
    public int nextWorld = 1 ;
    public int nextStage = 1 ;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(MoveTo(flag, poleBottom.position));
            //Di chuyển cờ đến đáy
            StartCoroutine(LevelCompleteSequence(other.transform));
            //Thực hiện chuỗi kết thúc màn
        }           
    }
    private IEnumerator LevelCompleteSequence(Transform player)
    {
        player.GetComponent<PlayerMovement>().enabled = false;  
        //vô hiệu hoá di chuyển nvat
        yield return MoveTo(player, poleBottom.position);
        yield return MoveTo(player, player.position + Vector3.right);
        yield return MoveTo(player, player.position + Vector3.right + Vector3.down);
        yield return MoveTo(player, castle.position);

        player.gameObject.SetActive(false);
        //Ẩn

        yield return new WaitForSeconds(2f);
        

        GameManager.Instance.LoadLevel(nextWorld, nextStage);
        

    }

    private IEnumerator MoveTo(Transform subject, Vector3 destination)
    {
        while (Vector3.Distance(subject.position, destination) > 0.125f)
        {
            //Nếu khoảng cách giữa đối tượng và điểm đến lớn hơn 0.125 đơn vị, tiếp tục di chuyển.
            subject.position = Vector3.MoveTowards(subject.position, destination, speed * Time.deltaTime);
            //di chuyển nvat đến đích 
            yield return null;
        }

        subject.position = destination;
        //Đặt vị trí obj là đích
    }
}

