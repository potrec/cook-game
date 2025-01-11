using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Start()
    {
        cuttingCounter.OnCut += CuttingCounter_OnCut;
    }
    
    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger("Cut");
    }
}
