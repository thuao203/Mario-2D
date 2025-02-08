using System.Collections;
using UnityEngine;

public class BlockHit : MonoBehaviour
{
    public GameObject item;
    //Tham chiếu đến một GameObject (ví dụ: đồng xu hoặc power-up)
    // sẽ xuất hiện khi khối bị đánh
    public Sprite emptyBlock;
    //Sprite dùng để thay thế khối khi đã bị đánh hết lần sử dụng.
    public int maxHits = -1;
    //Số lần khối có thể bị đánh. 
    //Giá trị -1 thường biểu thị "không giới hạn".
    private bool animating;
    //Biến cờ để kiểm soát hoạt ảnh. Ngăn người chơi
    // đánh khối khi hoạt ảnh đang chạy.
    private void OnCollisionEnter2D(Collision2D collision)
    //Xu li ca cham
    {
        if (!animating && maxHits != 0 && collision.gameObject.CompareTag("Player"))
        //Hoạt ảnh dừng, khối còn lượt, va chạm player
        {
            if (collision.transform.DotTest(transform, Vector2.up))
            {
                //Xác định va chạm từ phía dưới dùng dottest
                Hit();
            }
        }
    }
    private void Hit()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        //Lấy component SpriteRenderer để thao tác với sprite của khối.
        spriteRenderer.enabled = true;
        //spriteRenderer.enabled = true;

        maxHits--;
        if (maxHits == 0)
        {
            spriteRenderer.sprite = emptyBlock;
            //Đổi sprite của khối thành emptyBlock.
        }
        if (item != null)
        //Nếu khối có đối tượng item gắn kèm
        {
            Instantiate (item, transform.position, Quaternion.identity);
        //Tạo bản sao item tại vị trí hiện tại của khối, với góc quay mặc định.
        }
        StartCoroutine(Animate());
        //chạy hoạt ảnh khối khi bị đánh.
    }
    private IEnumerator Animate()
    {
        animating = true;
        Vector3 restingPosition = transform.localPosition;
        Vector3 animatedPosition = restingPosition + Vector3.up * 0.5f;

        yield return Move(restingPosition, animatedPosition);
        yield return Move(animatedPosition, restingPosition);

        animating = false;
        // Đánh dấu hoạt ảnh kết thúc.
    }
    private IEnumerator Move (Vector3 from, Vector3 to)
    {
        float elapsed = 0f;
        float duration = 0.125f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;

            transform.localPosition = Vector3.Lerp(from , to, t);
            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.localPosition = to;

    }
}
