using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private new Camera camera;
    private new Rigidbody2D rigidbody; //lực và tương tác vật lý cho nhân vật.
    private new Collider2D collider; //va chạm

    private Vector2 velocity; //vận tốc
    private float inputAxis; //ưu trữ đầu vào từ bàn phím cho chuyển động ngang.

    public float moveSpeed = 8f; //Tốc độ di chuyển của nhân vật
    public float maxJumpHeight = 5f; //Chiều cao tối đa mà nhân vật có thể nhảy.
    public float maxJumpTime = 1f; //Thời gian tối đa mà nhân vật có thể nhảy.
    public float jumpForce => (2f * maxJumpHeight) / (maxJumpTime/2f); //Tính toán lực nhảy
    public float gravity => (-2f *maxJumpHeight) / Mathf.Pow((maxJumpTime/2f),2); //Tính toán lực hấp dẫn

    public bool  grounded { get; private set; }
    public bool jumping { get; private set; }
    public bool running => Mathf.Abs(velocity.x) > 0.25f || Mathf.Abs(inputAxis) > 0.25f; //?chay
    public bool sliding => (inputAxis > 0f && velocity.x < 0f) || (inputAxis < 0f && velocity.x > 0f); //?truot
     
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();     
        camera = Camera.main;
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;  //bị ảnh hưởng bởi lực và va chạm.
        collider.enabled = true; //va chạm với các đối tượng khác.
        velocity = Vector2.zero;
        jumping = false;
    }

    private void OnDisable()
    {
        rigidbody.isKinematic = true;  //đối tượng bị vô hiệu hóa.
        collider.enabled = false; //ngăn chặn luc + va cham
        velocity = Vector2.zero;
        jumping = false;
    }

    private void Update()
    {
        HorizontalMovement(); //xử lý chuyển động ngang.
        grounded = rigidbody.Raycast(Vector2.down); //dung raycast xem vat co dung tren ground k
        if (grounded)
        {
            GroundedMovement(); //co tren dat thi nhay duoc
        }
        ApplyGravity(); //áp dụng trọng lực lên nhân vật.
    }

    private void HorizontalMovement()
    {
        inputAxis = Input.GetAxis("Horizontal");   //giá trị đầu vào từ bàn phím cho trục ngang (trái/phải).
        velocity.x = Mathf.MoveTowards(velocity.x, inputAxis * moveSpeed, moveSpeed * Time.deltaTime );//di chuyển nó về phía giá trị đầu vào, giới hạn tốc độ theo moveSpeed.
        if (rigidbody.Raycast(Vector2.right * velocity.x)) {
            velocity.x = 0f;
        }

        if (velocity.x >= 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (velocity.x < 0f )
        {
            transform.eulerAngles = new Vector3 (0f, 180f, 0f); //xoay nv
        }
    }

    private void GroundedMovement()
    {
        velocity.y = Mathf.Max (velocity.y, 0f);
        jumping = velocity.y > 0f;
        if (Input.GetButtonDown("Jump")) //nhan nhay,nv gan vt nhay va nhay
        {
            velocity.y = jumpForce;
            jumping = true;
        }
    }
    private void ApplyGravity()//Áp dụng trọng lực lên nhân vật
    {
        bool falling = velocity.y < 0f || !Input.GetButton("Jump"); //am thi dang roi || neu k jump : falling true, roi nhanh hon
        float multiplier = falling ? 2f : 1f; //roi 2f || chua roi or dang nhan jump g giu nguyen 

        velocity.y += gravity * multiplier * Time.deltaTime;//cong trong luc-> roi nhanh
        velocity.y = Mathf.Max(velocity.y, gravity / 2f);//k am qua muc, gioi han :  gravity / 2f
    }
    private void FixedUpdate()
    {
        Vector2 position = rigidbody.position; //Lấy vị trí hiện tại của nhân vật từ Rigidbody2D của nó.
        position += velocity * Time.fixedDeltaTime;
        //vận tốc hiện tại của nhân vật, được tính toán trước đó trong hàm ApplyGravity() và các hàm điều khiển khác.

        Vector2 leftEdge = camera.ScreenToWorldPoint(Vector2.zero);//Tọa độ (0, 0) của màn hình tương ứng với góc dưới bên trái.
        //camera.ScreenToWorldPoint(): Chuyển đổi tọa độ màn hình (pixel) thành tọa độ thế giới.
        Vector2 rightEdge = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        //Tọa độ (width, height) tương ứng với góc trên bên phải của màn hình
        position.x = Mathf.Clamp(position.x, leftEdge.x + 0.5f, rightEdge.x - 0.5f);
        //Giới hạn giá trị tọa độ X của nhân vật: nhân vật không đi ra ngoài rìa của màn hình.
        rigidbody.MovePosition (position);
    }
    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        //va chạm với đối tượng thuộc lớp "Enemy"
        {
            if (transform.DotTest(collision.transform, Vector2.down))
            {
                velocity.y = jumpForce/2f;
                // Nếu nhân vật dẫm lên đối tượng "Enemy", nhân vật sẽ bật lên với một nửa lực nhảy.
                jumping = true; 
            }
        }
        else if (collision.gameObject.layer != LayerMask.NameToLayer("PowerUp"))
        //Kiểm tra nếu đối tượng va chạm không phải là "PowerUp".
        {
            if(transform.DotTest(collision.transform, Vector2.up)){
                //cham khoi gach,... vận tốc theo trục Y sẽ bị reset về 0, nhân vật sẽ dừng di chuyển lên trên.
                velocity.y = 0f;
            }
        }
    }

}
