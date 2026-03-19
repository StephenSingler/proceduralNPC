using UnityEngine;
using System.Collections.Generic;

public class GrammarStoryGenerator : MonoBehaviour
{
    Dictionary<string, string[]> rules;

    void Awake() {
        BuildRules();
    }

    void BuildRules() {
        rules = new Dictionary<string, string[]>() {
            {"Quest", new[] {
                "Starting NPC: {NPC}\n\nStarting Location: {Location}\n\nObjective: {Objective}"
            }},

            {"NPC", new[]{
                "{npcAdj} {npcFirstName}",
                "{npcAdj} {npcLastName}",
                "{npcFirstName} {npcLastName}",
                "{npcFirstName} {npcTitle}",
                "{npcAdj} {npcFirstName} {npcTitle}",
                "{npcAdj} {npcLastName} {npcTitle}"
            }},

            {"Location", new[]{
                "The {locAdj} {locType}",
                "The {locType} {preposition} {landmark}",
                "The {locType} {preposition} {settlement}",
                "{NPC}'s {locType}"

            }},

            {"Objective", new[]{
                "Fetch {amount} {gather} for me",
                "Deliver this {parcel} to {NPC} in {Location}",
                "Escort me to {Location}",
                "Kill {amount} {target}",
                "Gather {amount} {gather} from {target} for me"
            }},
        };
    }

    public string Generate() {
        return Expand("Quest");
    }

    string Expand(string symbol, int depth = 0) {
        if (depth > 10)
            return "";

        if (!rules.ContainsKey(symbol))
            return symbol;
        
        string rule = RandomChoice(rules[symbol]);

        foreach (var key in rules.Keys) {
            string token = "{" + key + "}";
            if (rule.Contains(token)) {
                rule = rule.Replace(token, Expand(key, depth + 1));
            }
        }
        return rule;
    }

    string RandomChoice(string[] arr) {
        return arr[Random.Range(0, arr.Length)];
    }
}
