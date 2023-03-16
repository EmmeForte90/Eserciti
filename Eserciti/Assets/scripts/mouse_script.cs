using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class mouse_script : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler{
	public init init;
	public UnityEvent onLeftClick;
	public UnityEvent onRightClick;
	public UnityEvent onMiddleClick;
	
	/*
	public void Start(){
		init = gameObject.Find("script").GetComponent<init>();
	}
	*/

	//questo sembra funzionare per tutto ciò che si trova sulla canvas
	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left){
			//onLeftClick.Invoke();
			init.mouse_click(gameObject, "sx");
		}
		else if (eventData.button == PointerEventData.InputButton.Right){
			//onRightClick.Invoke();
			init.mouse_click(gameObject, "dx");
		}
		else if (eventData.button == PointerEventData.InputButton.Middle){
			//onMiddleClick.Invoke();
			init.mouse_click(gameObject, "mi");
		}
	}
	public void OnPointerEnter(PointerEventData eventData){init.mouse_enter(gameObject);}
	public void OnPointerExit(PointerEventData eventData){init.mouse_exit(gameObject);}
	
	//sembra che questo funzioni con gli sprite e tutto ciò che c'è nella mappa (esterno della canvas)
	//ah: devono avere un collider.........
	void OnMouseOver () {
		if (Input.GetMouseButtonDown(0)){init.mouse_click(gameObject, "sx");}
		if (Input.GetMouseButtonDown(1)){init.mouse_click(gameObject, "dx");}
	}
	void OnMouseEnter(){
		//print (gameObject);
		try{	//non ho idea per la quale, questa buffonata fà funzionare il tutto...
			init.mouse_enter(gameObject);
		}	
		catch{}
	}
	void OnMouseExit(){init.mouse_exit(gameObject);}
}