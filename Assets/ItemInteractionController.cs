using Unity.VisualScripting;
using UnityEngine;

public class ItemInteractionController : MonoBehaviour
{
    [SerializeField]
    private Camera Camera;
    [SerializeField]
    bool isHoldingItem;
    [SerializeField]
    Transform holdTransform;
    [SerializeField]
    GameObject DropButton;
    [SerializeField]
    float dropPower;

    private GameObject holdedObject;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isHoldingItem)
        {
            Vector3 clickPos = Input.mousePosition;

            Ray ray = Camera.main.ScreenPointToRay(clickPos);

            RaycastHit raycastHit;

            bool hitSometing = Physics.Raycast(ray, out raycastHit);


            if (hitSometing && (raycastHit.transform.gameObject.tag == "pickupable"))
            {
                Debug.Log("hit pickupable");
                isHoldingItem = true;
                raycastHit.transform.SetParent(holdTransform);
                raycastHit.transform.GetComponent<Rigidbody>().isKinematic = true;
                raycastHit.transform.GetComponent<Collider>().enabled = false;
                raycastHit.transform.localPosition = Vector3.zero;
                raycastHit.transform.localRotation = Quaternion.identity;
                DropButton.SetActive(true);
                holdedObject = raycastHit.transform.gameObject;
            }
        }
    }

    public void DropItem()
    {
        DropButton.SetActive(false);
        holdedObject.transform.GetComponent<Collider>().enabled = true;
        holdedObject.transform.GetComponent<Rigidbody>().isKinematic = false;
        holdedObject.transform.SetParent(null);
        holdedObject.transform.GetComponent<Rigidbody>().AddForce(transform.forward * dropPower,ForceMode.Impulse);
        isHoldingItem = false; 
    }

}