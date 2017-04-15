using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GS
{
    [CreateAssetMenu()]
    public class SoundSelector : ScriptableObject
    {
        public AudioClip[] clips;
        [Range(1, 10)]
        public int[] weights;
        [Range(0, 2f)]
        public float pitch;
        [Range(0, 100)]
        public int volume;
        int weightSum;

        void OnEnable()
        {
            weightSum = 0;
            foreach (int x in weights)
            {
                weightSum += x;
            }
        }
        public void Play(AudioSource source)
        {
            int x = Random.Range(0, weightSum);
            int sum = 0;
            int clip = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sum += weights[i];
                if (sum > x)
                {
                    clip = i;
                    break;
                }
            }
            if (clips.Length > 0)
            {
                source.clip = clips[clip];
                source.volume = volume;
                source.pitch = pitch;
                source.Play();
            }
        }
    }
}