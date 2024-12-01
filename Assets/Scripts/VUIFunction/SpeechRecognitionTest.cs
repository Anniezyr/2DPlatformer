using System.IO;
using HuggingFace.API;
using TMPro;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpeechRecognitionTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;   
    [SerializeField] private VolumeControl vc;     
    [SerializeField] private Image RecordImg;   

    private AudioClip clip;
    private byte[] bytes;
    private bool isRecording = false;
    private Coroutine flickerCoroutine;    

    private void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (!isRecording)
            {
                StartRecording();
            }
            else
            {
                StopRecording();
            }
        }

        // over 10 seconds, stop
        if (isRecording && Microphone.GetPosition(null) >= clip.samples)
        {
            StopRecording();
        }
    }

    private void StartRecording()
    {
        text.color = Color.white;
        text.text = "Recording...";
        clip = Microphone.Start(null, false, 10, 48000); 
        isRecording = true;

        
        if (flickerCoroutine == null)
        {
            flickerCoroutine = StartCoroutine(FlickerIcon());
        }
    }

    private void StopRecording()
    {
        if (!isRecording) return;

        int position = Microphone.GetPosition(null);
        Microphone.End(null);

        if (position > 0)
        {
            var samples = new float[position * clip.channels];
            clip.GetData(samples, 0);
            bytes = EncodeAsWAV(samples, clip.frequency, clip.channels);

            
            SendRecording();
        }

        isRecording = false;

        // stop
        if (flickerCoroutine != null)
        {
            StopCoroutine(flickerCoroutine);
            flickerCoroutine = null;
            RecordImg.color = new Color(212 / 255f, 83 / 255f, 83 / 255f); // 恢复默认颜色
        }
    }

    private void SendRecording()
    {
        text.color = Color.yellow;
        text.text = "Sending...";

        HuggingFaceAPI.AutomaticSpeechRecognition(bytes, response =>
        {
            text.color = Color.white;
            text.text = response;
            vc.AnalyzeArray(response);
            StartCoroutine(ClearTextAfterDelay(5f));
        }, error =>
        {
            text.color = Color.red;
            text.text = "API unstable.";
            StartCoroutine(ClearTextAfterDelay(5f));
        });
    }

    private IEnumerator FlickerIcon()
    {
        Color color1 = new Color(212 / 255f, 83 / 255f, 83 / 255f);
        Color color2 = new Color(215 / 255f, 30 / 255f, 30 / 255f);

        while (true)
        {
            RecordImg.color = color1;
            yield return new WaitForSeconds(0.5f);

            RecordImg.color = color2;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private byte[] EncodeAsWAV(float[] samples, int frequency, int channels)
    {
        using (var memoryStream = new MemoryStream(44 + samples.Length * 2))
        {
            using (var writer = new BinaryWriter(memoryStream))
            {
                writer.Write("RIFF".ToCharArray());
                writer.Write(36 + samples.Length * 2);
                writer.Write("WAVE".ToCharArray());
                writer.Write("fmt ".ToCharArray());
                writer.Write(16);
                writer.Write((ushort)1);
                writer.Write((ushort)channels);
                writer.Write(frequency);
                writer.Write(frequency * channels * 2);
                writer.Write((ushort)(channels * 2));
                writer.Write((ushort)16);
                writer.Write("data".ToCharArray());
                writer.Write(samples.Length * 2);

                foreach (var sample in samples)
                {
                    writer.Write((short)(sample * short.MaxValue));
                }
            }
            return memoryStream.ToArray();
        }
    }

    private IEnumerator ClearTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        text.text = string.Empty; // Clear the text
    }
}
