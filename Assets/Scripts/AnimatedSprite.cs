using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    public Sprite[] sprites;
    public float framerate = 1f / 6f; //6 hinh /1s

    private SpriteRenderer spriteRenderer;
    private int frame;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(Animate), framerate, framerate);
        //gọi sau một khoảng thời gian framerate và sau đó sẽ được gọi lại sau mỗi khoảng thời gian framerate.
    }

    private void OnDisable()
    {
        CancelInvoke(); //Hủy tất cả các cuộc gọi lặp lại đã được thiết lập trước đó với 
    }

    private void Animate () //cập nhật khung hình của hoạt ảnh.
    {
        frame++;

        if (frame >= sprites.Length){
            frame = 0;    
        }
         
        if (frame >= 0 && frame < sprites.Length) //Kiểm tra để đảm bảo rằng giá trị của frame hợp lệ
        {
            spriteRenderer.sprite = sprites[frame]; //Cập nhật sprite hiện tại của spriteRenderer bằng khung hình tương ứng trong mảng sprites.
        }
    }
}
