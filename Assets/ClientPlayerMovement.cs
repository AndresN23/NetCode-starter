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
            m_ThirdPersonController.enabled = true;
        }
    }
}
