using UnityEngine;
using FishNet.Object;
public class BodyRotation : NetworkBehaviour
{
    private PawnInput _input;
    private void Awake()
    {
        _input = GetComponentInParent<PawnInput>();
  
    }

    void Update()
    {
        if (!IsOwner) return;

        if (_input.horizontal > 0)
            transform.localScale = new Vector3(2f, 2f, 1f);
        else if (_input.horizontal < 0)
            transform.localScale = new Vector3(-2f, 2f, 1f);
    }
}
