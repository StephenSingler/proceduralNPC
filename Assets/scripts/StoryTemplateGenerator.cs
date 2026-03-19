using UnityEngine;
using System.Collections.Generic;

public class StoryTemplateGenerator : MonoBehaviour
{
    private Dictionary<string, List<string>> wordBank;

    void Awake() {
        BuildWordBank();
    }

    void BuildWordBank() {
        wordBank = new Dictionary<string, List<string>> {
            {"npcAdj", new List<string>{"Old", "Young", "Elder", "Quiet", "Loud", "Shady", "Honest", "Cowardly", "One-Eyed", "Mad"}},
            {"npcTitle", new List<string>{"the Forgotten", "the Lost", "the Forsaken", "the First", "the Second", "the Third", "the Last", "the Traitor", "the Brave", "the Kind", "the Just", "the Butcher", "the Hunted", "the Elder", "the Keeper"}},
            {"npcFirstName", new List<string>{"Duri", "Tun", "Eddul", "Ripley", "Trey", "Calvin", "Osmund", "Ervind", "Alfred", "Howe", "Rodolf", "Byrne", "Julius", "Haydon", "Malik", "Aggy", "Gabi", "Munira", "Mirla", "Oola", "Jennine", "Eva", "Naomi", "Valena", "Ami"}},
            {"npcLastName", new List<string>{"Torp", "Kent", "Claridge", "Netley", "Oldham", "Sherwood", "Presley", "Burton", "Blackwood", "Nash", "Breeden", "Marlowe", "Thorne", "Knottley", "Tyndall", "Lancaster", "Garrick", "Preston"}},
            {"locType", new List<string>{"Farm", "Well", "Road", "Cave", "Mine", "Camp", "Temple", "Crypt", "Tunnels"}},
            {"locAdj", new List<string>{"Old", "Ruined", "Broken", "Northern", "Southern", "Eastern", "Western", "Sunken", "High", "Low", "Green"}},
            {"preposition", new List<string>{"near", "below", "outside", "beneath", "of", "around", "in"}},
            {"settlement", new List<string>{"the {locAdj} {settlements}", "Ravenbrook", "Lastholde", "Strawbrook", "Oldmounte"}},
            {"settlements", new List<string>{"City", "Village", "Town", "Outpost"}},
            {"landmark", new List<string>{"the {locAdj} {landmarks}"}},
            {"landmarks", new List<string>{"Crossing", "Road", "Spire", "Tree", "Lighthouse"}},
            {"amount", new List<string>{"3", "5", "7", "10", "15", "20"}},
            {"gather", new List<string>{"apples", "herbs", "bones", "gems", "mushrooms", "berries", "teeth", "claws", "pelts", "supplies", "vials", "relics"}},
            {"parcel", new List<string>{"tome", "letter", "parcel", "package", "report", "plea", "request", "answer", "research"}},
            {"target", new List<string>{"wolves", "goblins", "bandits", "spiders", "skeletons", "cultists", "rats", "ogres", "dragons", "elementals", "constructs"}}
        };
    }

    public string GenerateFromTemplate(string template) {
        foreach (var key in wordBank.Keys) {
            string token = "{" + key + "}";

            while (template.Contains(token)) {
                template = ReplaceFirst(template, token, RandomChoice(wordBank[key]));
            }
        }
        return template;
    }

    public void AddWord(string key, string value) {
        if (!wordBank.ContainsKey(key))
            wordBank[key] = new List<string>();
        
        wordBank[key].Clear();
        wordBank[key].Add(value);
    }

    string RandomChoice(List<string> list) {
        return list[Random.Range(0, list.Count)];
    }

    string ReplaceFirst(string text, string search, string replace) {
        int pos = text.IndexOf(search);
        if (pos < 0) return text;

        return text.Remove(pos, search.Length).Insert(pos, replace);
    }
}
