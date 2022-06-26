using UnityEngine;

public class SwerveMovement : MonoBehaviour
{
    [SerializeField] private float swerveSpeed = .5f;
    [SerializeField] private float maxSwerveAmount = 2;
    [SerializeField] private float maxHorizontalDistance = 2;

    // Tiklama ile hizli yer degisiminin onune gecer.
    // TODO: katsayilari kontrol et.
    [SerializeField] private bool checkDistanceChange = true;
    [SerializeField] private float maxHorizontalChange = .5f;

    private float deltaPos;
    private float lastMousePosX;
    private float lastPositonChange;

    private void Update()
    {
        if (GameManager.instance.isContinue==true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                // oyunu baþlatýyoruz.. karakter ileri hareket etmeye baþlýyor..
                //if (!GameManager.instance.isContinue ) {

                //}

                lastMousePosX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButton(0))
            {
                deltaPos = Input.mousePosition.x - lastMousePosX;
                lastMousePosX = Input.mousePosition.x;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                deltaPos = 0;
            }

            var swerve = Time.deltaTime * swerveSpeed * deltaPos;
            swerve = Mathf.Clamp(swerve, -maxSwerveAmount, maxSwerveAmount);

            var x = transform.position.x + swerve;
            if (x < maxHorizontalDistance && x > -maxHorizontalDistance)
                if (checkDistanceChange)
                {
                    if (Mathf.Abs(x - lastPositonChange) < maxHorizontalChange) transform.Translate(swerve, 0, 0);
                }
                else
                    transform.Translate(swerve, 0, 0);

            lastPositonChange = x;
        }
    }
}
