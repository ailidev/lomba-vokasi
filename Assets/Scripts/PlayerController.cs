using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private float m_speed = 5.0f;
    private float m_moveX, m_moveY;
    private float m_xRotation, m_yRotation;
    private float m_lookSensitivity = 3.0f;
    private Vector3 m_moveHorizontal, m_moveVertical, m_velocity;
    private Vector3 m_rotation;
    private Vector3 m_cameraRotation;
    private Rigidbody m_rigibody;
    private Camera m_camera;
    public bool m_cursorIsLocked = true;
    public float lockRotation = 90.0f;

    private void Start() {
        m_rigibody = GetComponent<Rigidbody>();
        m_camera = GetComponentInChildren<Camera>();
    }

    private void Update() {
        m_moveX = Input.GetAxis("Horizontal");
        m_moveY = Input.GetAxis("Vertical");

        m_moveHorizontal = transform.right * m_moveX;
        m_moveVertical = transform.forward * m_moveY;

        m_velocity = (m_moveHorizontal + m_moveVertical).normalized * m_speed;

        // Mouse movement
        m_yRotation = Input.GetAxisRaw("Mouse X");
        m_rotation = new Vector3(0, m_yRotation, 0) * m_lookSensitivity;

        m_xRotation = Input.GetAxisRaw("Mouse Y");
        m_cameraRotation = new Vector3(m_xRotation, 0 ,0) * m_lookSensitivity;

        // Move player
        if (m_velocity != Vector3.zero) {
            m_rigibody.MovePosition(m_rigibody.position + m_velocity * Time.fixedDeltaTime);
        }

        if (m_rotation != Vector3.zero) {
            m_rigibody.MoveRotation(m_rigibody.rotation * Quaternion.Euler(m_rotation));
        }

        if (m_camera != null) {
            m_camera.transform.Rotate(-m_cameraRotation);
        }

        InternalLockUpdate();
    }

    private void InternalLockUpdate() {
        if (Input.GetKeyUp(KeyCode.Escape)) {
            m_cursorIsLocked = false;
        } else if (Input.GetMouseButtonUp(0)) {
            m_cursorIsLocked = true;
        }

        if (m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else if (!m_cursorIsLocked) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

}
