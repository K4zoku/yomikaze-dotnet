using Yomikaze.Domain.Entities;

namespace Yomikaze.Infrastructure.Database;

public partial class YomikazeDbContext
{
    private static partial class Default
    {
        public static readonly Genre[] Genres =
        [
            new Genre
            {
                Id = 1,
                Name = "Action",
                Description =
                    "A story that focuses on physical action, such as fighting, war, sports, or physical challenges."
            },
            new Genre
            {
                Id = 2,
                Name = "Adventure",
                Description =
                    "Explores exotic locations and tense situations, such as battles, a treasure hunt, or an exploration of the unknown."
            },
            new Genre
            {
                Id = 3,
                Name = "Comedy",
                Description = "A story with humorous narration or dialogue, intended to amuse the audience."
            },
            new Genre
            {
                Id = 4,
                Name = "Drama",
                Description =
                    "A story that is neither a comedy nor a tragedy, typically focusing on a conflict between the protagonist and antagonist."
            },
            new Genre
            {
                Id = 5,
                Name = "Fantasy",
                Description =
                    "A story that takes place in a setting that defies the laws of the universe, such as magic or supernatural elements."
            },
            new Genre
            {
                Id = 6,
                Name = "Horror",
                Description = "A story that evokes fear in both the characters and the audience."
            },
            new Genre
            {
                Id = 7, Name = "Mystery", Description = "A story that revolves around solving a puzzle or a crime."
            },
            new Genre
            {
                Id = 8,
                Name = "Psychological",
                Description =
                    "A story that emphasizes the psychology of its characters and their unstable emotional states."
            },
            new Genre { Id = 9, Name = "Romance", Description = "A story about love." },
            new Genre
            {
                Id = 10,
                Name = "Slice of Life",
                Description = "A story that portrays a \"cut-out\" sequence of events in a character's life."
            },
            new Genre
            {
                Id = 11,
                Name = "Sports",
                Description = "A story that revolves around sports, such as baseball or basketball."
            },
            new Genre
            {
                Id = 12,
                Name = "Supernatural",
                Description = "A story that involves supernatural elements, such as ghosts or demons."
            },
            new Genre
            {
                Id = 13,
                Name = "Thriller",
                Description = "A story that is fast-paced and suspenseful, often involving a crime."
            },
            new Genre { Id = 14, Name = "Tragedy", Description = "A story that ends in a tragic or unhappy way." }
        ];
    }
}