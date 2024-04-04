using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalCameraHandler : MonoBehaviour
{
    //�ܼ��� ī�޶� ������Ʈ�� �ٿ� ����ϸ� ��Ʈ��ũ ��Ȳ�� ���� �Ҷ� ���� �� �ִ�
    //LocalCameraHandler�� ���� �����ؼ� �ε巯�� ī�޶� �������� ����

    public Transform cameraAnchorPoint;

    Vector2 viewInput;
    float cameraRotationX = 0;
    float cameraRotationY = 0;

    //other components
    NetworkCharacterControllerPrototypeCustom networkCharacterControllerPrototypeCustom;
    public Camera localCamera;

    private void Awake(){
        localCamera = GetComponent<Camera>();
        networkCharacterControllerPrototypeCustom = GetComponentInParent<NetworkCharacterControllerPrototypeCustom>();
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraRotationX = GameManager.instance.cameraViewRotation.x;
        cameraRotationY = GameManager.instance.cameraViewRotation.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {   
        if(cameraAnchorPoint == null)
            return;
        if(!localCamera.enabled)
            return;

        //player�� ��ġ(cameraAnchorPoint)�� ī�޶� �̵�
        localCamera.transform.position = cameraAnchorPoint.position;
        cameraRotationX += viewInput.y * Time.deltaTime * networkCharacterControllerPrototypeCustom.viewUpDownRotationSpeed;
        cameraRotationX = Math.Clamp(cameraRotationX,-90,90);
        cameraRotationY += viewInput.x * Time.deltaTime * networkCharacterControllerPrototypeCustom.rotationSpeed;

        localCamera.transform.rotation = Quaternion.Euler(cameraRotationX,cameraRotationY,0);
    }
    public void SetViewInputVector(Vector2 viewInput){
        this.viewInput = viewInput;
    }

    private void OnDestroy(){
        if(cameraRotationX != 0 && cameraRotationY != 0){
            GameManager.instance.cameraViewRotation.x = cameraRotationX;
            GameManager.instance.cameraViewRotation.y = cameraRotationY;
        }
    }
}

