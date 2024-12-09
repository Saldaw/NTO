using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public List<AudioClip> musicTracks; // Список музыкальных треков
    public AudioSource audioSource1;    // Первый аудиоисточник
    public AudioSource audioSource2;    // Второй аудиоисточник

    private int currentTrackIndex = 0;  // Индекс текущего трека
    private bool isAudioSource1Playing = true; // Указывает, какой аудиоисточник активен

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
        audioSource.volume = 1f; // Начальная громкость
    }

    private IEnumerator CheckAndCrossfade()
    {
        while (true)
        {
            AudioSource activeSource = isAudioSource1Playing ? audioSource1 : audioSource2;
            AudioSource inactiveSource = isAudioSource1Playing ? audioSource2 : audioSource1;

            // Ждем, пока до конца текущего трека не останется 10 секунд
            float timeUntilSwitch = activeSource.clip.length - activeSource.time - 10f;
            if (timeUntilSwitch < 0) timeUntilSwitch = 0;
            yield return new WaitForSeconds(timeUntilSwitch);

            // Запускаем следующий трек на неактивном источнике
            int nextTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
            PlayTrack(inactiveSource, nextTrackIndex);

            // Плавный переход между треками
            StartCoroutine(CrossfadeTracks(activeSource, inactiveSource));

            // Обновляем текущий индекс трека и переключаем активный источник
            currentTrackIndex = nextTrackIndex;
            isAudioSource1Playing = !isAudioSource1Playing;
        }
    }

    private IEnumerator CrossfadeTracks(AudioSource fromSource, AudioSource toSource)
    {
        float duration = 5f; // Длительность плавного перехода
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            fromSource.volume = Mathf.Lerp(1f, 0f, t); // Уменьшаем громкость текущего источника
            toSource.volume = Mathf.Lerp(0f, 1f, t);   // Увеличиваем громкость следующего источника
            yield return null;
        }

        fromSource.Stop(); // Останавливаем предыдущий трек после завершения перехода
    }
}