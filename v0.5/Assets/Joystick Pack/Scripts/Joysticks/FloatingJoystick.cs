using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    public Animator animator;
    private void Awake()
    {
        animator = FindObjectOfType<PlayerController>().GetComponentInChildren<Animator>();
    }
    protected override void Start()
    {
        base.Start();
        background.gameObject.SetActive(false);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.anchoredPosition = ScreenPointToAnchoredPosition(eventData.position);
        background.gameObject.SetActive(true);
        base.OnPointerDown(eventData);
        animator.SetBool("isRunning",true);
        Debug.Log("yürüyo");
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        base.OnPointerUp(eventData);
        animator.SetBool("isRunning", false);
        Debug.Log("durdu");

    }
}