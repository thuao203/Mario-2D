using UnityEngine;
[RequireComponent(typeof(SpriteRenderer)) ]
//Đảm bảo GameObject gắn script này phải có SpriteRenderer.
//Nếu không có, Unity sẽ tự động thêm SpriteRenderer.
public class BG_MainLevel : MonoBehaviour
{
    [SerializeField]
    //Cho phép chỉnh sửa dù pri trong Inspector
    private float cameraSpeedX;
    [SerializeField]
    private float cameraSpeedY;
    private float startPositionX;
    private float startPositionY;
    private float spriteSizeX;
    private float spriteSizeY;
    //Kích thước sprite theo trục X và Y
    private Transform cameraTransform;
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        startPositionX = transform.position.x;
        //Ghi lại vị trí ban đầu của nền theo trục X
        startPositionY = transform.position.y;
        // Ghi lại vị trí ban đầu của nền theo trục Y
        spriteSizeX = GetComponent<SpriteRenderer>().bounds.size.x;
        //Lấy chiều rộng của sprite từ SpriteRenderer
// spriteSizeY = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        float relativeDist = cameraTransform.position.x * cameraSpeedX;
        //Tính khoảng cách di chuyển nền theo camera trên trục X.
        float relativeDistY = cameraTransform.position.x * cameraSpeedY;
        //Tính khoảng cách di chuyển nền theo camera trên trục X.
        transform.position = new Vector3( startPositionX + relativeDist, startPositionY + relativeDistY, transform.position.z );
        //Cập nhật vị trí của nền theo vị trí camera, tạo hiệu ứng parallax (nền chuyển động chậm hơn camera)
        float relativeCameraDist =  cameraTransform.position.x * ( 1 - cameraSpeedX );
        //Tính khoảng cách tương đối giữa camera và nền để xác định khi nào cần lặp lại sprite.
        if (relativeCameraDist > startPositionX + spriteSizeX )
        //Nếu camera đã đi xa hơn cạnh phải của sprite.
        {
            startPositionX += spriteSizeX;
            //Dịch chuyển vị trí ban đầu của sprite sang phải một sprite nữa.
        }
        else if (relativeCameraDist < startPositionX - spriteSizeX )
        //Nếu camera đã đi xa hơn cạnh trái của sprite.
        {
            startPositionX -= spriteSizeX;
            //Dịch chuyển vị trí ban đầu của sprite sang trái một sprite nữa.
        }
    }
}
