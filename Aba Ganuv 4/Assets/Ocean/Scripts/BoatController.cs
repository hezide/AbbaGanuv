using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoatController : MonoBehaviour{

    [SerializeField] private List<GameObject> m_motors;

	[SerializeField] private bool m_enableAudio = true;
	[SerializeField] private AudioSource m_boatAudioSource;
	[SerializeField] private float m_boatAudioMinPitch = 0.4F;
	[SerializeField] private float m_boatAudioMaxPitch = 1.2F;

	[SerializeField] public float m_FinalSpeed = 100F;
	[SerializeField] public float m_InertiaFactor = 0.005F;
	[SerializeField] public float m_turningFactor = 2.0F;
    [SerializeField] public float m_accelerationTorqueFactor = 35F;
	[SerializeField] public float m_turningTorqueFactor = 35F;

	private float m_verticalInput = 0F;
	private float m_horizontalInput = 0F;
    private Rigidbody m_rigidbody;
	private Vector2 m_androidInputInit;

	private float accel=0;
	private float accelBreak;

     void Start()  {
        m_rigidbody = GetComponent<Rigidbody>();
   	    accelBreak = m_FinalSpeed*0.3f;
	}

	void Update()	{
		setInputs (Input.GetAxisRaw("Vertical"), Input.GetAxisRaw("Horizontal"));
	}

	public void setInputs(float iVerticalInput, float iHorizontalInput)	{
		m_verticalInput = iVerticalInput;
		m_horizontalInput = iHorizontalInput;
	}

	 void FixedUpdate()	{

		if(m_verticalInput>0) {
			if(accel<m_FinalSpeed) { accel+=(m_FinalSpeed * m_InertiaFactor); accel*=m_verticalInput;}
		} else if(m_verticalInput==0) {
			if(accel>0) { accel-=m_FinalSpeed * m_InertiaFactor; }
			if(accel<0) { accel+=m_FinalSpeed * m_InertiaFactor; }
		}else if(m_verticalInput<0){
			if(accel>-accelBreak) { accel-=m_FinalSpeed * m_InertiaFactor*2;  }
		}
		
		m_rigidbody.AddRelativeForce(Vector3.forward  * accel);

        m_rigidbody.AddRelativeTorque(
			m_verticalInput * -m_accelerationTorqueFactor,
			m_horizontalInput * m_turningFactor,
			m_horizontalInput * -m_turningTorqueFactor
        );

        if(m_motors.Count > 0) {

            float motorRotationAngle = 0F;
			float motorMaxRotationAngle = 70;

			motorRotationAngle = - m_horizontalInput * motorMaxRotationAngle;

			for(int i=0; i<m_motors.Count; i++) {
				float currentAngleY = m_motors[i].transform.localEulerAngles.y;
				if (currentAngleY > 180.0f)
					currentAngleY -= 360.0f;

				float localEulerAngleY = Lerp(currentAngleY, motorRotationAngle, Time.deltaTime * 10);
				m_motors[i].transform.localEulerAngles = new Vector3(
					m_motors[i].transform.localEulerAngles.x,
					localEulerAngleY,
					m_motors[i].transform.localEulerAngles.z
				);
            }
        }
		
		if (m_enableAudio && m_boatAudioSource != null) 
		{
			
			float pitchLevel =  m_boatAudioMaxPitch*Mathf.Abs(m_verticalInput);
			if(m_verticalInput<0) pitchLevel*=0.7f;

			if (pitchLevel < m_boatAudioMinPitch) pitchLevel = m_boatAudioMinPitch;


			float smoothPitchLevel = Lerp(m_boatAudioSource.pitch, pitchLevel, Time.deltaTime*0.5f);

			m_boatAudioSource.pitch = smoothPitchLevel;
		}
    }

	static float Lerp (float from, float to, float value) {
		if (value < 0.0f) return from;
		else if (value > 1.0f) return to;
		return (to - from) * value + from;
	}

}
