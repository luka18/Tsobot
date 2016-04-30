using UnityEngine;
using System.Collections;

public class CallLightning : MonoBehaviour {




    public void LightningStrike(Vector3 ori, Vector3 dest)
    {
        print("STRIKLE WTF BRAH");
        DigitalRuby.ThunderAndLightning.LightningBoltScript script = gameObject.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltScript>();
        int count = 4;
        float duration = 0.25f;//0.25
        float delay = 0.3f;
        System.Random r = new System.Random();
        Vector3 start = ori;
        Vector3 end = dest;
        int generations = 6;
        float chaosFactor = 0.2f;
        float trunkWidth = 0.5f;
        float glowIntensity = 0.1793651f;
        float glowWidthMultiplier = 4f;
        float forkedness = 0.5f;
        float singleDuration = Mathf.Max(1.0f / 30.0f, (duration / (float)count));
        float fadePercent = 0.15f;
        float growthMultiplier = 0f;

        while (count-- > 0)
        {
            DigitalRuby.ThunderAndLightning.LightningBoltParameters parameters = new DigitalRuby.ThunderAndLightning.LightningBoltParameters
            {
                Start = start,
                End = end,
                Generations = generations,
                LifeTime = (count == 1 ? singleDuration : (singleDuration * (((float)r.NextDouble() * 0.4f) + 0.8f))),
                Delay = delay,
                ChaosFactor = chaosFactor,
                TrunkWidth = trunkWidth,
                GlowIntensity = glowIntensity,
                GlowWidthMultiplier = glowWidthMultiplier,
                Forkedness = forkedness,
                Random = r,
                FadePercent = fadePercent, // set to 0 to disable fade in / out
                GrowthMultiplier = growthMultiplier
            };
            script.CreateLightningBolt(parameters);
            delay += (singleDuration * (((float)r.NextDouble() * 0.8f) + 0.4f));
        }
    }
}
