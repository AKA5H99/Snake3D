using UnityEngine;
using UnityEngine.EventSystems;

public class FixedTouchField_Android : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [HideInInspector]
    protected int PointerId;
    [HideInInspector]
    public bool Pressed;
    [HideInInspector]
    public float XPos;

    void Update()
    {
        FuncitionT();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }


    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }


    public void FuncitionT()
    {

        if(Pressed)
        {
            XPos = Input.touches[PointerId].position.x;
            Debug.Log(XPos);
        }
        else
        {
            XPos = 0;
        }

    }
}
