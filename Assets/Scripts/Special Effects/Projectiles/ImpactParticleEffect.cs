using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ImpactParticlesConfig
{
    public LayerMask onLayers;
    public Material particleMaterial;
    public int particleCount;
    [Tooltip("Mesh to render in the specified collision case. If null, Particles will be rendered in Stretched Billboard mode as default.")]
    public Mesh particleMesh;
}

public class ImpactParticleEffect : MonoBehaviour
{
    private ParticleSystem particles;
    private ParticleSystemRenderer particlesRenderer;

    [SerializeField] private List<ImpactParticlesConfig> particlesPerLayer;

    void Start()
    {
        if (!TryGetComponent<ParticleSystem>(out particles)) { Debug.LogError("No ParticleSystem found for ImpactParticleEffect!"); }
        else { particlesRenderer = particles.GetComponent<ParticleSystemRenderer>(); }
    }


    public void PlayOnImpact(Collision collision)
    {
        // ~~when projectile hits a wall, refer to a call here to determine the particles to play
        foreach (ImpactParticlesConfig config in particlesPerLayer)
        {
            // if the layer collided with is specified in the current config, then the current config tells us what particles to play
            // (not sure if it works as intended 100% of the time)
            if ((1<<collision.gameObject.layer & config.onLayers) != 0)
            {
                particlesRenderer.material = config.particleMaterial;
                if (config.particleMesh != null)
                {
                    particlesRenderer.renderMode = ParticleSystemRenderMode.Mesh;
                    particlesRenderer.mesh = config.particleMesh;
                }
                else
                {
                    particlesRenderer.renderMode = ParticleSystemRenderMode.Stretch;
                }


                var emitParams = new ParticleSystem.EmitParams();
                emitParams.applyShapeToPosition = true;
                emitParams.position = collision.transform.position;
                //emitParams.velocity = collision.transform.forward;  // ~~hmmmm (test)
                

                particles.Emit(emitParams, config.particleCount);
                break;
            }
        }
    }
}
