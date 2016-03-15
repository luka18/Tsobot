using UnityEngine;
using System.Collections;

public class testlight : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Sprint"))
        {
            CallLight();
        }

	
	}
    void CallLight()
    {
        // Important, make sure this script is assigned properly, or you will get null ref exceptions.
        DigitalRuby.ThunderAndLightning.LightningBoltScript script = gameObject.GetComponent<DigitalRuby.ThunderAndLightning.LightningBoltScript>();
        int count = 1;
        float duration = 0.25f;
        float delay = 0.0f;
        int seed = 1539394787;
        System.Random r = new System.Random(seed);
        Vector3 start = new Vector3(0, 4f, 0f);
        Vector3 end = new Vector3(0f, 0f,0f);
        int generations = 6;
        float chaosFactor = 0.2f;
        float trunkWidth = 5f;
        float glowIntensity = 0f;
        float glowWidthMultiplier = 0f;
        float forkedness = 0.5f;
        float singleDuration = Mathf.Max(1.0f / 30.0f, (duration / (float)count));
        float fadePercent = 0.1587301f;
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


