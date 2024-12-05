using UnityEngine;

public class ChangeOfDayAndNight : MonoBehaviour
{
    [Range(0, 1)]
    [SerializeField] float TimeOfDay;
    [SerializeField] float DayDurationInSecond = 30f;

    void Update()
    {
        TimeOfDay += Time.deltaTime / DayDurationInSecond;
        if (TimeOfDay >= 1)
        {
            TimeOfDay -= 1;
        }
        transform.localRotation = Quaternion.Euler(TimeOfDay * 360f, 180, 0);
    }
}
