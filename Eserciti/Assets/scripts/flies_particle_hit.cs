using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flies_particle_hit : MonoBehaviour
{
    public ParticleSystem part;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    void OnParticleCollision(GameObject other)
    {
        print ("coll with "+other.name);
    }
}
