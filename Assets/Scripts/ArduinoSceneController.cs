using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO.Ports; // If this shows an error, check Step 1 above!

public class ArduinoSceneController : MonoBehaviour
{
    [Header("Serial Settings")]
    [Tooltip("Check Device Manager to find your Arduino's port (e.g. COM3 or /dev/tty.usbmodem)")]
    public string portName = "COM5"; 
    public int baudRate = 9600;

    [Header("Scene Mapping")]
    public string sceneA = "SceneA_Name";
    public string sceneB = "SceneB_Name";

    private SerialPort stream;

    void Start()
    {
        // Open the connection
        stream = new SerialPort(portName, baudRate);
        stream.ReadTimeout = 50;
        
        try {
            stream.Open();
            Debug.Log("Serial Port Opened Successfully");
        }
        catch (System.Exception e) {
            Debug.LogError("Could not open serial port: " + e.Message);
        }
    }

    void Update()
    {
        if (stream.IsOpen)
        {
            try
            {
                // Read the line sent by Arduino
                string data = stream.ReadLine().Trim();

                if (data == "SCENE_A")
                {
                    Debug.Log("Arduino triggered Scene A");
                    SceneManager.LoadScene(sceneA);
                }
                else if (data == "SCENE_B")
                {
                    Debug.Log("Arduino triggered Scene B");
                    SceneManager.LoadScene(sceneB);
                }
            }
            catch (System.TimeoutException)
            {
                // This is normal, it just means no data was sent this frame
            }
        }
    }

    void OnApplicationQuit()
    {
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }
    }
}