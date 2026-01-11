using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] PlayerController player;

    private ParticleSystem particles;
    ParticleSystem.EmissionModule emission;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        emission = particles.emission;
    }

    private void Update()
    {
        if (player.IsGrounded() && !player.IsDead())
        {
            emission.enabled = true;
        }
        else if (!player.IsGrounded() || player.IsDead())
        {
            emission.enabled = false;
        }
    }
}
