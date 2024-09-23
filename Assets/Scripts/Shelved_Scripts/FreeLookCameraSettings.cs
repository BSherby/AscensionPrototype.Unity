using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[System.Serializable]
public class FreeLookCameraSettings
{
    public float topRigRadius;
    public float middleRigRadius;
    public float bottomRigRadius;

    public float topRigHeight;
    public float middleRigHeight;
    public float bottomRigHeight;

    public float xAxisSpeed;
    public float yAxisSpeed;

    public FreeLookCameraSettings(CinemachineFreeLook freeLookCamera)
    {
        topRigRadius = freeLookCamera.m_Orbits[0].m_Radius;
        middleRigRadius = freeLookCamera.m_Orbits[1].m_Radius;
        bottomRigRadius = freeLookCamera.m_Orbits[2].m_Radius;

        topRigHeight = freeLookCamera.m_Orbits[0].m_Height;
        middleRigHeight = freeLookCamera.m_Orbits[1].m_Height;
        bottomRigHeight = freeLookCamera.m_Orbits[2].m_Height;

        xAxisSpeed = freeLookCamera.m_XAxis.m_MaxSpeed;
        yAxisSpeed = freeLookCamera.m_YAxis.m_MaxSpeed;
    }

    public void ApplyToCamera(CinemachineFreeLook freeLookCamera)
    {
        freeLookCamera.m_Orbits[0].m_Radius = topRigRadius;
        freeLookCamera.m_Orbits[1].m_Radius = middleRigRadius;
        freeLookCamera.m_Orbits[2].m_Radius = bottomRigRadius;

        freeLookCamera.m_Orbits[0].m_Height = topRigHeight;
        freeLookCamera.m_Orbits[1].m_Height = middleRigHeight;
        freeLookCamera.m_Orbits[2].m_Height = bottomRigHeight;

        freeLookCamera.m_XAxis.m_MaxSpeed = xAxisSpeed;
        freeLookCamera.m_YAxis.m_MaxSpeed = yAxisSpeed;
    }
}
