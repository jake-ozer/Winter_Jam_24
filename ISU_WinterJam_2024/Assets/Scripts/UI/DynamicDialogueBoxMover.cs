using UnityEngine;

public class DynamicDialogueBoxMover : MonoBehaviour
{
    public void MoveBoxUpByOneFactor()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y + 50, transform.position.z);
    }

    public void MoveBoxDownByOneFactor()
    {
        this.transform.position = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
    }
}
