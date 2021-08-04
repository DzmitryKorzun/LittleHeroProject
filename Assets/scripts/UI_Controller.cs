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
    public Text countOfHealthBottleText;
    public Text countOfManaBottleText;
    public Text countOfMoneyBottleText;
    public Image selectionArrowImage;
    public Button shopButton;
    public GameObject shopPanel;
    private PersonController personController;
    private Transform selectionArrowImageTransformCache;
    private Vector3 swordImagePos, fireballImagePos, bombImagePos;
    private float offsetAlongThe_Y_axisFromThePicture = 120;
    private int typeSelectedWeapon = 1;
    private WeaponController weaponController;
    public GameObject weaponsObj;
    private float amountOfMana;
    IWeapon[] weapons; 
    private void Awake()
    {
        Inventory.singltone.InventoryStateChange += updateAllInventoryText;
        weapons = new IWeapon[] { weaponsObj.GetComponent<Ultimate>(), weaponsObj.GetComponent<Sword>(), weaponsObj.GetComponent<FireBall>(), weaponsObj.GetComponent<Bomb>() };
    }
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
        personController.ShopApproachEvent += ChangeOfShopStatus;

        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref bombCountText, 5f, 100);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref fireballCountText, 12f, 100);

    }

    public void Attack1_Click()  //FireButton 
    {
        weaponController.TypeOfWeapon(weapons, typeSelectedWeapon);
    }
    
    public void Attack2_Click() //Ultimate
    {
        weaponController.TypeOfWeapon(weapons, 0);
    }

    private void changeHealth(float health)
    {
        healthImage.fillAmount = health / 100;
    }

    private void changeMana(float mana)
    {
        this.amountOfMana = mana;
        manaImage.fillAmount = mana/ 100;
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref bombCountText, 12f, mana);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref fireballCountText, 5f, mana);
    }

    private void ChangeOfShopStatus(bool isEntered)
    {
        if (isEntered)
        {
            shopButton.gameObject.SetActive(true);
        }
        else
        {
            shopButton.gameObject.SetActive(false);
            shopPanel.SetActive(false);
        }
    }

    public void swordTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = swordImagePos;
        typeSelectedWeapon = 1;
    }
    public void fireballTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = fireballImagePos;
        typeSelectedWeapon = 2;
    }

    public void bombTypeWeaponSelected()
    {
        selectionArrowImageTransformCache.position = bombImagePos;
        typeSelectedWeapon = 3;
    }

    public void toggleShopWindowActivity()
    {
        shopPanel.SetActive(!shopPanel.activeSelf);
    }

    public void updateAllInventoryText(int money, int health, int mana)
    {
        countOfMoneyBottleText.text = money.ToString();
        countOfHealthBottleText.text = health.ToString();
        countOfManaBottleText.text = mana.ToString();
    }



}
