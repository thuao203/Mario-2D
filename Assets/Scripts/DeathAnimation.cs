using System.Collections;
using UnityEngine;

public class DeathAnimation : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; 
    public Sprite deadSprite;

    private void Reset ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable ()
    {
        UpdateSprite();
        //Cập nhật hình ảnh để chuyển sang trạng thái chết.
        DisablePhysics();
        //Vô hiệu hóa các thành phần vật lý liên quan đến đối tượng.
        StartCoroutine(Animate());
        //Bắt đầu hoạt ảnh cái chết
    }

    private void UpdateSprite ()
    {
        spriteRenderer.enabled = true;
        spriteRenderer.sortingOrder = 10;//thứ tự sắp xếp của sprite, đảm bảo rằng nó sẽ hiển thị ở trên cùng
        if (deadSprite != null)
        //Nếu deadSprite được gán, thay đổi sprite của đối tượng thành deadSprite.
        {
            spriteRenderer.sprite = deadSprite;
        }
    }

    private void DisablePhysics () // tắt tất cả các thành phần vật lý liên quan đến đối tượng
    {
        Collider2D[] colliders = GetComponents<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
        GetComponent<Rigidbody2D>().isKinematic = true; //đối tượng sẽ không bị ảnh hưởng bởi vật lý 
        
        PlayerMovement playerMovement = GetComponent<PlayerMovement>();
        EntityMovement entityMovement = GetComponent<EntityMovement>();
        //vô hiệu hóa chúng để chặn việc điều khiển di chuyển.

        if (playerMovement != null )
        {
            playerMovement.enabled = false ;  //ngăn chặn nhân vật di chuyển.
        }
        if (entityMovement != null )
        {
            entityMovement.enabled = false ; //ngăn chặn nhân vật di chuyển.
        }
    }   

    private IEnumerator Animate ()
    {
        float elapsed = 0f;
        float duration = 3f;
        float jumpVelocity = 10f; //vận tốc theo phương y là 10.
        float gravity = -36f; //trọng lực

        Vector3 velocity = Vector3.up * jumpVelocity; //vận tốc ban đầu của đối tượng theo hướng lên.

        while(elapsed < duration)
        {
            transform.position += velocity * Time.deltaTime;  //Cập nhật vị trí của đối tượng, di chuyển nó theo vận tốc hiện tại
            velocity.y += gravity * Time.deltaTime; //thêm ảnh hưởng của trọng lực vào vận tốc
            elapsed += Time.deltaTime; 
            yield return null;
        }
    }
}
