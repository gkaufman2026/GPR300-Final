using UnityEngine;

public class TimeController : MonoBehaviour {
    [SerializeField] private float secondsPerDay = 10f;
    private float time;

    private bool isCycling = true;

    // Update is called once per frame
    void Update() {
        if (isCycling) {
            time = Time.deltaTime;
            gameObject.transform.Rotate(new Vector3(Mathf.Lerp(0, 360, time / secondsPerDay), 0, 0));
            if (time > secondsPerDay) {
                time = 0;
            }
        }
    }

    public float GetSecondsPerDay() => secondsPerDay;
    public void SetSecondsPerDay(float duration) => secondsPerDay = duration;
    public bool GetIsCycling() => isCycling;
    public void SetIsCycling(bool param) => isCycling = param;
}
