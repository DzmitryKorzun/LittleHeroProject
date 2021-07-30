using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{

    public Image healthImage;
    public Image manaImage;
    public Image swordWeaponImageUI;
    public Image fireballWeaponImageUI;
    public Image bombWeaponImageUI;
    public Text fireballCountText;
    public Text bombCountText;
    public Image selectionArrowImage;
    private PersonController personController;
    private Transform selectionArrowImageTransformCache;
    private Vector3 swordImagePos, fireballImagePos, bombImagePos;
    private float offsetAlongThe_Y_axisFromThePicture = 120;
    private int typeSelectedWeapon;
    private WeaponController weaponController;
    IWeapon[] weapons = new IWeapon[] { new Sword(), new FireBall(), new Bomb() };

    void Start()
    {
        PersonController.singlton.healthChange += changeHealth;
        PersonController.singlton.manaUse += changeMana;
        personController = PersonController.singlton;
        selectionArrowImageTransformCache = selectionArrowImage.GetComponent<Transform>();

        swordImagePos = new Vector2(swordWeaponImageUI.transform.position.x, swordWeaponImageUI.transform.position.y+ offsetAlongThe_Y_axisFromThePicture);
        fireballImagePos = new Vector2(fireballWeaponImageUI.transform.position.x, fireballWeaponImageUI.transform.position.y + offsetAlongThe_Y_axisFromThePicture);
        bombImagePos = new Vector2(bombWeaponImageUI.transform.position.x, bombWeaponImageUI.transform.position.y + offsetAlongThe_Y_axisFromThePicture);
        weaponController = new WeaponController(PersonController.singlton.animator, PersonController.singlton.heroTransform);

        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref bombCountText, 5f, 100);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref fireballCountText, 12f, 100);

    }

    public void Attack1_Click()
    {
        weaponController.TypeOfWeapon(weapons, 0);
    }
    
    public void Attack2_Click()
    {
        personController.Attack2();
    }

    private void changeHealth(float health)
    {
        healthImage.fillAmount = health / 100;
    }

    private void changeMana(float mana)
    {
        manaImage.fillAmount = mana/ 100;
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref bombCountText, 12f, mana);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref fireballCountText, 5f, mana);
    }

    public void swordTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = swordImagePos;
        typeSelectedWeapon = 0;
    }

    public void fireballTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = fireballImagePos;
        typeSelectedWeapon = 1;
    }

    public void bombTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = bombImagePos;
        typeSelectedWeapon = 2;
    }


}
