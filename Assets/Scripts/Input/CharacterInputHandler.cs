using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CharacterInputHandler : MonoBehaviour
{
    // �÷��̾��� �Է��� ó���ϰ� �� �Է��� ��Ʈ��ũ�� ���� �ٸ� �÷��̾�� ����ȭ�ϱ� ���� �����͸� �غ�
    // CharacterInputHandler�� �÷��̾�κ��� �Է��� �޾� LocalCameraHandler�� NetworkInputData�� ����
    // ����ڷκ��� �̵�, ����, ī�޶� ȸ�� ���Է����� ����
    // ������ ���ÿ� LocalCameraHandler�� �ٸ� ���� ���� ��ҷ� �����Ͽ� ���� �� ĳ������ �����Ӱ� ī�޶� ����
    // GetNetworkInput() �޼��带 ���� ��Ʈ��ũ�� ���� ����ȭ�� �Է� ������(NetworkInputData)�� �����ϰ� �ʱ�ȭ
    
    Vector2 moveInputVector = Vector2.zero;
    Vector2 viewInputVector = Vector2.zero;
    bool isJumpButtonPressed = false;

    //other components
    //CharacterMovementHandler characterMovementHandler;
    LocalCameraHandler localCameraHandler;

    private void Awake(){
        //characterMovementHandler = GetComponent<CharacterMovementHandler>();
        localCameraHandler = GetComponentInChildren<LocalCameraHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //View Input
        viewInputVector.x = Input.GetAxis("Mouse X");
        viewInputVector.y = Input.GetAxis("Mouse Y")*-1;

        //LocalCameraHandler�� ���� Update�� rotation�� �ƴ϶� ��Ʈ��ũ�� aim�� ����
        //characterMovementHandler.SetViewInputVector(viewInputVector);

        //Move Input
        //�� ������ ���� sync�� ������� �Ʒ�ó�� ���� ����
        moveInputVector.x = Input.GetAxis("Horizontal");
        moveInputVector.y = Input.GetAxis("Vertical");

        //Shooting, jump���� Update�� GetNetworkInput�� sync �ȸ¾� �Ʒ�ó�� �Ұ���
        //isJumpButtonPressed = Input.GetButtonDown("Jump");
        //�̷��� �����ؾ���
        if(Input.GetButtonDown("Jump"))
            isJumpButtonPressed = true;

        //Set View
        localCameraHandler.SetViewInputVector(viewInputVector);
    }

    public NetworkInputData GetNetworkInput(){
        NetworkInputData networkInputData = new NetworkInputData();
        //View data
        //networkInputData.rotationInput = viewInputVector.x;

        //Aim data
        networkInputData.aimForwardVector = localCameraHandler.transform.forward;
        //Move data
        networkInputData.movementInput = moveInputVector;

        networkInputData.isJumpPressed = isJumpButtonPressed;
        isJumpButtonPressed = false;

        return networkInputData;
    }
}
