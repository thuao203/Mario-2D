using System.Collections;
using UnityEngine;

public class BlockCoin : MonoBehaviour
{
    private void Start()
    {
        GameManager.Instance.AddCoin();
        //Gọi phương thức AddCoin() từ GameManager
        //GameManager.Instance có khả năng là một Singleton, 
        //cung cấp cách truy cập duy nhất vào quản lý trò chơi.
        StartCoroutine(Animate());
        //BlockCoin kich hoat thi bat dau 1 coroutine Animate()
    }
    private IEnumerator Animate()
    {
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 2f;

        yield return Move(restingPosition, animatedPosition);
        //Gọi coroutine Move() để di chuyển BlockCoin từ vị trí ban đầu đến vị trí nâng lên.
        yield return Move(animatedPosition, restingPosition);
        //Di chuyển ngược lại về vị trí ban đầu.
        Destroy(gameObject);
        //pha huy
    }
    private IEnumerator Move (Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        //Biến đếm thời gian đã trôi qua.
        float duration = 0.25f;
        //Thời gian để di chuyển từ from đến to

        while (elapsed < duration)
        //Vòng lặp chạy cho đến khi thời gian trôi qua bằng duration.
        {
            float t = elapsed / duration;
            //Tỉ lệ thời gian đã trôi qua so với tổng thời gian.
            transform.localPosition = Vector3.Lerp(from , to, t);
            //Tính toán vị trí hiện tại dựa trên t.
            //Vector3.Lerp(from, to, t): Nội suy tuyến tính giữa from và to dựa trên tỉ lệ t
            elapsed += Time.deltaTime;
            //Tăng thời gian đã trôi qua mỗi khung hình.
            yield return null;
            //Dừng coroutine tại đây, tiếp tục ở khung hình tiếp theo.
        }
        transform.localPosition = to;
        //Đảm bảo vị trí cuối cùng là to để tránh sai lệch.


    }
}
