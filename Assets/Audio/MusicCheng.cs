using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> musicTracks; // ������ ����������� ������
    public AudioSource audioSource1;    // ������ �������������
    public AudioSource audioSource2;    // ������ �������������

    private int currentTrackIndex = 0;  // ������ �������� �����
    private bool isAudioSource1Playing = true; // ���������, ����� ������������� �������

    private void Start()
    {
        if (musicTracks.Count > 0)
        {
            PlayTrack(audioSource1, currentTrackIndex);
            StartCoroutine(CheckAndCrossfade());
        }
    }

    private void PlayTrack(AudioSource audioSource, int trackIndex)
    {
        audioSource.clip = musicTracks[trackIndex];
        audioSource.Play();
        audioSource.volume = 1f; // ��������� ���������
    }

    private IEnumerator CheckAndCrossfade()
    {
        while (true)
        {
            AudioSource activeSource = isAudioSource1Playing ? audioSource1 : audioSource2;
            AudioSource inactiveSource = isAudioSource1Playing ? audioSource2 : audioSource1;

            // ����, ���� �� ����� �������� ����� �� ��������� 10 ������
            float timeUntilSwitch = activeSource.clip.length - activeSource.time - 10f;
            if (timeUntilSwitch < 0) timeUntilSwitch = 0;
            yield return new WaitForSeconds(timeUntilSwitch);

            // ��������� ��������� ���� �� ���������� ���������
            int nextTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
            PlayTrack(inactiveSource, nextTrackIndex);

            // ������� ������� ����� �������
            StartCoroutine(CrossfadeTracks(activeSource, inactiveSource));

            // ��������� ������� ������ ����� � ����������� �������� ��������
            currentTrackIndex = nextTrackIndex;
            isAudioSource1Playing = !isAudioSource1Playing;
        }
    }

    private IEnumerator CrossfadeTracks(AudioSource fromSource, AudioSource toSource)
    {
        float duration = 5f; // ������������ �������� ��������
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            fromSource.volume = Mathf.Lerp(1f, 0f, t); // ��������� ��������� �������� ���������
            toSource.volume = Mathf.Lerp(0f, 1f, t);   // ����������� ��������� ���������� ���������
            yield return null;
        }

        fromSource.Stop(); // ������������� ���������� ���� ����� ���������� ��������
    }
}