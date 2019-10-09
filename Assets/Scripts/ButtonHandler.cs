using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public int number;
    public GameManagerScript gameManager;
    
    // Start is called before the first frame update
    public void Click()
    {
        gameManager.ButtonClick(number);
    }
}
