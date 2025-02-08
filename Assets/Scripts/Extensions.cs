using UnityEngine;

public static class Extensions 
{
    private static LayerMask layerMask = LayerMask.GetMask("Default");
    //kiểm tra va chạm với các đối tượng thuộc layer "Default".
    public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction)
    {
        if (rigidbody.isKinematic){
            //không xử lý va chạm
            return false;
        }

        float radius = 0.25f;
        //Bán kính của hình tròn kiểm tra va chạm.
        float distance = 0.375f;
        //kich thuoc va tam xa cua vong tron va cham

        RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position,radius, direction.normalized, distance, layerMask);
        //Quét một hình tròn từ vị trí của rigidbody theo hướng direction.
        return hit.collider != null && hit.rigidbody != rigidbody;
        //hit.collider != null: Kiểm tra xem CircleCast có chạm vào bất kỳ đối tượng nào không.
        //hit.rigidbody != rigidbody: Đảm bảo không va chạm với chính đối tượng..
    }

    public static bool DotTest (this Transform transform, Transform other, Vector2 testDirection)
    {
        //kiểm tra xem va chạm xảy ra từ hướng nào
        Vector2 direction = other.position - transform.position;
        //Tính toán hướng từ đối tượng hiện tại đến đối tượng khác.
        return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
        // đối tượng khác nằm trong phạm vi của hướng kiểm tra
    }
}
