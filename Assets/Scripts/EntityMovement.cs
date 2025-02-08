using UnityEngine;

public class EntityMovement : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = Vector2.left;
    //Hướng di chuyển mặc định là sang trái.

    private new Rigidbody2D rigidbody;
    private Vector2 velocity;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        enabled = false;
        //chỉ hoạt động khi đối tượng nằm trong tầm nhìn camera.

    }
    private void OnBecameVisible()
    {
        enabled = true;
        //Kích hoạt script khi đối tượng nằm trong tầm nhìn của camera.
    }
    private void OnBecameInvisible()
    {
        enabled = false;
        //Vô hiệu hóa script khi đối tượng ra khỏi tầm nhìn.
    }
    private void OnEnable()
    {
        rigidbody.WakeUp();
        //Đánh thức rigidbody để bắt đầu tính toán vật lý.
    }
    private void OnDisable()
    {
        rigidbody.velocity = Vector2.zero;
        rigidbody.Sleep();
    }
    private void FixedUpdate()
    {
        velocity.x = direction.x * speed;
        velocity.y += Physics2D.gravity.y * Time.fixedDeltaTime;
        // Cập nhật vận tốc theo trục Y dựa trên trọng lực 

        rigidbody.MovePosition(rigidbody.position + velocity * Time.fixedDeltaTime);
        //Di chuyển đối tượng dựa trên vận tốc và thời gian (Time.fixedDeltaTime)
        if (rigidbody.Raycast(direction)){
            //Chạm tường quay lại
            direction = -direction;
        }

        if (rigidbody.Raycast(Vector2.down)){
            //Chạm đất giới hạn vY để k rơi
            velocity.y = Mathf.Max(velocity.y, 0);
        }
    }

}
