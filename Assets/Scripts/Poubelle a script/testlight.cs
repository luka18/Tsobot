using UnityEngine;
using System.Collections;

public class testlight : MonoBehaviour {



	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Jump"))
        {
            CallLight();
            print("called");
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
        System.Random r = new System.Random();
        Vector3 start = new Vector3(-3.5f, 2, 0);
        Vector3 end = new Vector3(3.5f, 2, 0);
        int generations = 6;
        float chaosFactor = 0.075f;
        float trunkWidth = 0.05f;
        float glowIntensity = 0.1f;
        float glowWidthMultiplier = 4f;
        float forkedness = 0.3f;
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


