using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] ParticleSystem SuccessVFX;
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Start Platform":
               
                break;
            case "Finish Platform":
                FindObjectOfType<GameSession>().LevelCompletedProcess();
                break;
            default:
                FindObjectOfType<GameSession>().GameOverProcess(); 
                break;
        }
    }

    void DestroyShip()
    {
        Destroy(gameObject);
    }

    public void StartExplosionVFX()
    {
        explosionVFX.Play();   
    }

    public void StartSuccessVFX()
    {
        SuccessVFX.Play();
    }
}
