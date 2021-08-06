using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class JoystickController : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler, IUpdateSelectedHandler
{
    public static JoystickController singltone;
    public Image joystickBack;
    public Image joystickFront;
    private Vector2 inputVector;

    void Awake()
    {
        if (!singltone)
        {
            singltone = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }
    public virtual void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);        
    }

    public virtual void OnPointerUp(PointerEventData ped)
    {
        PersonController.singlton.isMove = false;
        inputVector = Vector2.zero;
        joystickFront.rectTransform.anchoredPosition = Vector2.zero;
    }

    public virtual void OnDrag(PointerEventData ped)
    {
        PersonController.singlton.isMove = true;
        Vector2 pos;
        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBack.rectTransform, ped.position, ped.pressEventCamera, out pos))
        {
            pos.x = (pos.x / joystickBack.rectTransform.sizeDelta.x) + 0.5f;
            pos.y = (pos.y / joystickBack.rectTransform.sizeDelta.x) + 0.5f;
            inputVector = new Vector2(pos.x*2 - 1, pos.y*2 - 1);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
            joystickFront.rectTransform.anchoredPosition = new Vector2(inputVector.x * (joystickBack.rectTransform.sizeDelta.x/2), inputVector.y * joystickBack.rectTransform.sizeDelta.y/2);
        }
    }
    public float Horizontal()
    {
        if (inputVector.x != 0){
            return inputVector.x;
        }
        else{
            return Input.GetAxis("Horizontal");
        }


    }
    public float Vertical()
    {
        if (inputVector.y != 0)
        {
            return inputVector.y;
        }
        else
        {
            return Input.GetAxis("Vertical");
        }

    }

    public void OnUpdateSelected(BaseEventData eventData)
    {        
        Debug.Log(eventData.selectedObject);
    }


    
}
