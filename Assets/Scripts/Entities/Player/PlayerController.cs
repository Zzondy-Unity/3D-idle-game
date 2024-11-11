using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Inputs Inputs { get; private set; }
    public Inputs.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        Inputs = new Inputs();
        playerActions = Inputs.Player;
    }

    private void OnEnable()
    {
        Inputs.Enable();
    }

    private void OnDisable()
    {
        Inputs.Disable();
    }
}