using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Context;

public partial class YomikazeDbContext
{
    private static partial class Default
    {
        public static readonly Tag[] Genres =
        [
            new Tag
            {
                Name = "Action",
                Description =
                    "A story that focuses on physical action, such as fighting, war, sports, or physical challenges."
            },
            new Tag
            {
                Name = "Adventure",
                Description =
                    "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown."
            },
            new Tag
            {
                Name = "Comedy",
                Description = "A story with humorous narration or dialogue, intended to amuse the audience."
            },
            new Tag
            {
                Name = "Drama",
                Description =
                    "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist."
            },
            new Tag
            {
                Name = "Fantasy",
                Description =
                    "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements."
            },
            new Tag
            {
                Name = "Horror", Description = "A story that evokes fear in both the characters and the audience."
            },
            new Tag { Name = "Mystery", Description = "A story that revolves around solving a puzzle or a crime." },
            new Tag
            {
                Name = "Psychological",
                Description =
                    "A story that emphasizes the psychology of its characters and their unstable emotional states."
            },
            new Tag { Name = "Romance", Description = "A story about love." },
            new Tag
            {
                Name = "Slice of Life",
                Description = "A story that portrays a \"cut-out\" sequence of events in a character's life."
            },
            new Tag
            {
                Name = "Sports",
                Description = "A story that revolves around sports, such as baseball or basketball."
            },
            new Tag
            {
                Name = "Supernatural",
                Description = "A story that involves supernatural elements, such as ghosts or demons."
            },
            new Tag
            {
                Name = "Thriller",
                Description = "A story that is fast-paced and suspenseful, often involving a crime."
            },
            new Tag { Name = "Tragedy", Description = "A story that ends in a tragic or unhappy way." }
        ];
    }
}