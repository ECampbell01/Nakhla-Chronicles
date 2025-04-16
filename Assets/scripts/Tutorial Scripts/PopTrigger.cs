using System.Linq.Expressions;
using UnityEngine;

public class PopTrigger : MonoBehaviour
{
    public Pop popupManager;
    public string message;
    private bool hasTriggered = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasTriggered && collision.CompareTag("Player"))
        {
            hasTriggered = true;
            popupManager.animator.SetTrigger("pop");
            popupManager.PopUp(message);
        }
    }

}


