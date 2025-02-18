using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System.Collections;

public enum WeaponType
{
    RIFLE,
    SHOTGUN,
    GRENADE,
    SNIPER,
    COUNT
}
public abstract class Weapon : MonoBehaviour
{
    public GameObject weaponPrefab;
    public GameObject shooter;
    public int magazineSize = 10;
    public int currentAmmo;
    public float shootCooldown = 0.2f;
    public float reloadCooldown = 1.5f;
    private bool canShoot = true;
    private bool isReloading = false;

    public UnityEvent OnShoot = new UnityEvent();
    public UnityEvent OnReload = new UnityEvent();
    public abstract void Shoot(Vector3 direction, float speed);
    private void Start()
    {
        currentAmmo = magazineSize;
    }

    public void TryShoot(Vector3 direction, float speed)
    {
        if (canShoot && !isReloading && currentAmmo > 0)
        {
            StartCoroutine(ShootCoroutine(direction, speed));
        }
        else if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ShootCoroutine(Vector3 direction, float speed)
    {
        canShoot = false;
        currentAmmo--;
        Shoot(direction, speed);
        OnShoot?.Invoke();
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        OnReload?.Invoke();
        yield return new WaitForSeconds(reloadCooldown);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}
public class Rifle : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        GameObject bullet = GameObject.Instantiate(weaponPrefab);
        bullet.transform.position = shooter.transform.position + direction * 0.75f;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        bullet.GetComponent<SpriteRenderer>().color = Color.white;
        Object.Destroy(bullet, 1.0f);
    }
}
public class Shotgun : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        GameObject bullet = GameObject.Instantiate(weaponPrefab);
        GameObject bulletLeft = GameObject.Instantiate(weaponPrefab);
        GameObject bulletRight = GameObject.Instantiate(weaponPrefab);
        Vector3 directionLeft = Quaternion.Euler(0.0f, 0.0f, 30.0f) * direction;
        Vector3 directionRight = Quaternion.Euler(0.0f, 0.0f, -30.0f) * direction;
        bullet.transform.position = shooter.transform.position + direction * 0.75f;
        bulletLeft.transform.position = shooter.transform.position + directionLeft * 0.75f;
        bulletRight.transform.position = shooter.transform.position + directionRight * 0.75f;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed;
        bulletLeft.GetComponent<Rigidbody2D>().velocity = directionLeft * speed;
        bulletRight.GetComponent<Rigidbody2D>().velocity = directionRight * speed;
        bullet.GetComponent<SpriteRenderer>().color = Color.green;
        bulletLeft.GetComponent<SpriteRenderer>().color = Color.green;
        bulletRight.GetComponent<SpriteRenderer>().color = Color.green;
        Destroy(bullet, 1.0f);
        Destroy(bulletLeft, 1.0f);
        Destroy(bulletRight, 1.0f);
    }
}
public class GrenadeLauncher : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        GameObject grenade = GameObject.Instantiate(weaponPrefab);
        grenade.transform.position = shooter.transform.position + direction * 0.75f;
        grenade.GetComponent<Rigidbody2D>().velocity = direction * speed;
        Destroy(grenade, 1.0f);
    }
}
public class Sniper : Weapon
{
    public override void Shoot(Vector3 direction, float speed)
    {
        GameObject bullet = GameObject.Instantiate(weaponPrefab);
        bullet.transform.position = shooter.transform.position + direction * 0.75f;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * speed * 2.0f;
        bullet.GetComponent<SpriteRenderer>().color = Color.red;
        Destroy(bullet, 2.0f);
    }
}

public class Weapons : MonoBehaviour
{
    [SerializeField]
    GameObject bulletPrefab;
    [SerializeField]
    GameObject grenadePrefab;
    float bulletSpeed = 10.0f;
    WeaponType weaponType = WeaponType.RIFLE;
    Weapon rifle = new Rifle();
    Weapon shotgun = new Shotgun();
    Weapon grenadeLauncher = new GrenadeLauncher();
    Weapon sniper = new Sniper();
    void Start()
    {
        rifle = gameObject.AddComponent<Rifle>();
        shotgun = gameObject.AddComponent<Shotgun>();
        grenadeLauncher = gameObject.AddComponent<GrenadeLauncher>();
        sniper = gameObject.AddComponent<Sniper>();

        rifle.weaponPrefab = bulletPrefab;
        rifle.shooter = gameObject;
        shotgun.weaponPrefab = bulletPrefab;
        shotgun.shooter = gameObject;
        grenadeLauncher.weaponPrefab = bulletPrefab;
        grenadeLauncher.shooter = gameObject;
        sniper.weaponPrefab = bulletPrefab;
        sniper.shooter = gameObject;
    }
    void Update()
    {
        // Directional movement
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.down;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }
        direction = direction.normalized;
        float moveSpeed = 5.0f;
        Vector3 movement = direction * moveSpeed * Time.deltaTime;
        transform.position += movement;
        Vector3 mouse = Input.mousePosition;
        mouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector3 mouseDirection = (mouse - transform.position).normalized;
        Debug.DrawLine(transform.position, transform.position + mouseDirection * 5.0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            switch (weaponType)
            {
                case WeaponType.RIFLE:
                    rifle.TryShoot(mouseDirection, bulletSpeed);
                    break;
                case WeaponType.SHOTGUN:
                    shotgun.TryShoot(mouseDirection, bulletSpeed);
                    break;
                case WeaponType.GRENADE:
                    grenadeLauncher.TryShoot(mouseDirection, bulletSpeed);
                    break;
                case WeaponType.SNIPER:
                    sniper.TryShoot(mouseDirection, bulletSpeed);
                    break;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            int weaponNumber = (int)weaponType + 1;
            weaponNumber %= (int)WeaponType.COUNT;
            weaponType = (WeaponType)weaponNumber;
            Debug.Log("Selected weapon: " + weaponType);
        }
    }
    void ShootShotgun(Vector3 direction)
    {
        GameObject bullet = Instantiate(bulletPrefab);
        GameObject bulletLeft = Instantiate(bulletPrefab);
        GameObject bulletRight = Instantiate(bulletPrefab);
        Vector3 directionLeft = Quaternion.Euler(0.0f, 0.0f, 30.0f) * direction;
        Vector3 directionRight = Quaternion.Euler(0.0f, 0.0f, -30.0f) * direction;
        bullet.transform.position = transform.position + direction * 0.75f;
        bulletLeft.transform.position = transform.position + directionLeft * 0.75f;
        bulletRight.transform.position = transform.position + directionRight * 0.75f;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        bulletLeft.GetComponent<Rigidbody2D>().velocity = directionLeft * bulletSpeed;
        bulletRight.GetComponent<Rigidbody2D>().velocity = directionRight * bulletSpeed;
        bullet.GetComponent<SpriteRenderer>().color = Color.green;
        bulletLeft.GetComponent<SpriteRenderer>().color = Color.green;
        bulletRight.GetComponent<SpriteRenderer>().color = Color.green;
        Destroy(bullet, 1.0f);
        Destroy(bulletLeft, 1.0f);
        Destroy(bulletRight, 1.0f);
    }
    void ShootGrenade(Vector3 direction)
    {
        GameObject grenade = Instantiate(grenadePrefab);
        grenade.transform.position = transform.position + direction * 0.75f;
        grenade.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        Destroy(grenade, 1.0f);
    }
}