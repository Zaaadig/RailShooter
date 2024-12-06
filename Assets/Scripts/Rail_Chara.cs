using UnityEngine;
using UnityEngine.Splines;

public class Rail_Chara : MonoBehaviour
{
    [SerializeField] private SplineAnimate m_splineAnimate;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_splineAnimate.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
