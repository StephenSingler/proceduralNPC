using UnityEngine;

public class GrammarUIController : MonoBehaviour
{
    [Header("Systems")]
    public GrammarStoryGenerator grammar;
    public StoryTemplateGenerator templateGen;

    public string  GenerateStory() {
        string story = grammar.Generate();
        while (story.Contains('{')) {
            story = templateGen.GenerateFromTemplate(story);
        }

        return story;
    }
}
