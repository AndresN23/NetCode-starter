using UnityEngine;
using Unity.Netcode;
using StarterAssets;
using UnityEngine.InputSystem;

public class ClientPlayerMovement : NetworkBehaviour
{
    [SerializeField] private PlayerInput m_PlayerInput;
    [SerializeField] private StarterAssetsInputs m_StarterAssestsInputs;
    [SerializeField] private ThirdPersonController m_ThirdPersonController;

    private void Awake()
    {
        m_StarterAssestsInputs.enabled = false;
        m_PlayerInput.enabled = false;
        m_ThirdPersonController.enabled = false;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();

        if (IsOwner)
        {
            m_StarterAssestsInputs.enabled = true;
            m_PlayerInput.enabled = true;

        }

        if (IsServer)
        {
            m_ThirdPersonController.enabled = true;
        }
    }

    [Rpc(SendTo.Server)]
    private void UpdateInputServerRpc(Vector2 move, Vector2 look, bool jump, bool sprint)
    {
        m_StarterAssestsInputs.MoveInput(move);
        m_StarterAssestsInputs.LookInput(look);
        m_StarterAssestsInputs.JumpInput(jump);
        m_StarterAssestsInputs.SprintInput(sprint);
    }
    private void LateUpdate()
    {
        if (!IsOwner)
            return;

        UpdateInputServerRpc(m_StarterAssestsInputs.move, m_StarterAssestsInputs.look, m_StarterAssestsInputs.jump, m_StarterAssestsInputs.sprint);
    }
}
