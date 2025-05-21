using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator m_Animator;
    int index;
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        index = Random.Range(0, 2);

    }
    void Update()
    {
        if (GameManager.instance.isVictory == true)
        {
            m_Animator.SetFloat("Blend", 1f);
        }
        else if(GameManager.instance.isVictory == false && InGameTimer.instance.isGameEnded)
        { 
            m_Animator.SetFloat("Blend", 1.5f);
        }
        else
        {
            switch (index)
            {
                case 0:
                    m_Animator.SetFloat("Blend", 0);
                    break;
                case 1:
                    m_Animator.SetFloat("Blend", 0.5f);
                    break;
                case 2:
                    m_Animator.SetFloat("Blend", 1);
                    break;
            }

        }
    }
}
