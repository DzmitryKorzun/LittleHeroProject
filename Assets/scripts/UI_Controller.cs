using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Text countOfKills;
    public Image selectionArrowImage;
    public Button shopButton;
    public Button repeatButton;
    public Button exitButton;
    public GameObject deathPanel;
    public GameObject shopPanel;
    private PersonController personController;
    private Transform selectionArrowImageTransformCache;
    private Vector3 swordImagePos, fireballImagePos, bombImagePos;
    private float offsetAlongThe_Y_axisFromThePicture;
    private int typeSelectedWeapon = 1;
    private WeaponController weaponController;
    public GameObject weaponsObj;
    private float amountOfMana;
    private float amountOfKills = 0;

    IWeapon[] weapons; 
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        EventController.gameRepeat += unsubscribingFromAnEvent;
        EventController.InventoryStateChange += updateAllInventoryText;
        weapons = new IWeapon[] { weaponsObj.GetComponent<Ultimate>(), weaponsObj.GetComponent<Sword>(), weaponsObj.GetComponent<FireBall>(), weaponsObj.GetComponent<Bomb>() };
    }
    void Start()
    {
        EventController.healthChange += changeHealth;
        EventController.manaUse += changeMana;
        EventController.skeletDead += skeletDeathCountUI;
        EventController.deadEvent += personDead;
        EventController.ShopApproach += ChangeOfShopStatus;
        personController = PersonController.singlton;
        selectionArrowImageTransformCache = selectionArrowImage.GetComponent<Transform>();
        offsetAlongThe_Y_axisFromThePicture = swordWeaponImageUI.GetComponent<RectTransform>().sizeDelta.x / 2 + 40;
        Debug.Log(offsetAlongThe_Y_axisFromThePicture);
        swordImagePos = new Vector2(swordWeaponImageUI.transform.position.x, swordWeaponImageUI.transform.position.y+ offsetAlongThe_Y_axisFromThePicture);
        fireballImagePos = new Vector2(fireballWeaponImageUI.transform.position.x, fireballWeaponImageUI.transform.position.y + offsetAlongThe_Y_axisFromThePicture);
        bombImagePos = new Vector2(bombWeaponImageUI.transform.position.x, bombWeaponImageUI.transform.position.y + offsetAlongThe_Y_axisFromThePicture);
        weaponController = new WeaponController(PersonController.singlton.animator, PersonController.singlton.heroTransform);
        deathPanel.SetActive(false);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref bombCountText, Bomb.manaCost, 100);
        ControlOfThePossibilityOfUsingTheSelectedWeapon.numberOfPossibleUses(ref fireballCountText, FireBall.manaCost, 100);
    }

    public void Attack1_Click()  //FireButton 
    {
        weaponController.TypeOfWeapon(weapons, typeSelectedWeapon);
    }
    
    private void personDead()
    {
        deathPanel.SetActive(true);
    }

    public void repeatButton_click()
    {
        deathPanel.SetActive(false);
        EventController.gameRestartEvent();
        SceneManager.LoadScene(1);
        
    }

    public void exitButton_click()
    {
        Application.Quit();
    }


    private void skeletDeathCountUI()
    {
        amountOfKills++;
        countOfKills.text = $"{amountOfKills}/50";
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

    private void unsubscribingFromAnEvent()
    {
        EventController.healthChange -= changeHealth;
        EventController.manaUse -= changeMana;
        EventController.skeletDead -= skeletDeathCountUI;
        EventController.deadEvent -= personDead;
        EventController.ShopApproach -= ChangeOfShopStatus;
        EventController.gameRepeat -= unsubscribingFromAnEvent;
        EventController.InventoryStateChange -= updateAllInventoryText;
    }

}
