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

        if (_input.mouseX > 0)
            transform.localScale = new Vector3(1.23f, 1.55f, 1f);
        else
            transform.localScale = new Vector3(-1.23f, -1.55f, 1f);
    }
}
