using System.Text.Json;
using System.Text.Json.Serialization;
using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Models;
using MapperProfile = AutoMapper.Profile;

namespace Yomikaze.Application.Data.Configs;

public class YomikazeMapper : MapperProfile
{
    public YomikazeMapper() 
    {
        
        CreateMap<string, ulong>().ConvertUsing((src) => IdParse(src));
        CreateMap<ulong, string>().ConvertUsing((src) => src.ToString());
        CreateMap<BaseModel, BaseEntity>()
            .ForMember(dest => dest.Id, options => options.Ignore())
            .ForMember(dest => dest.CreationTime, options => options.Ignore())
            .ForMember(dest => dest.LastModified, options => options.Ignore())
            .ForMember(dest => dest.WorkerId, options => options.Ignore())
            .ForAllMembers(options => options.Condition((_, _, member) => member != null));
        
        CreateMap<ChapterModel, Chapter>()
            .ForMember(dest => dest.Pages, options =>
            {
                options.Condition(src => src.Pages is { Count: > 0 });
                options.MapFrom(src => (src.Pages ?? new List<string>()).Select(page => new Page { Image = page }));
            })
            .ForMember(dest => dest.ComicId, options =>
            {
                options.Condition(src => src.ComicId != null);
                options.MapFrom(src => IdParse(src.ComicId));
            });
        CreateMap<Chapter, ChapterModel>()
            .ForMember(dest => dest.Pages, options => options.MapFrom(src => src.Pages.Select(page => page.Image)))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()));

        CreateMap<CoinPricingModel, CoinPricing>().ReverseMap();

        CreateMap<ComicModel, Comic>()
            .ForMember(dest => dest.Id, options => options.Condition(src => src.Id != null))
            .ForMember(dest => dest.ComicTags, options =>
            {
                options.Condition(src => src.TagIds != null && src.TagIds.Count != 0);
                options.MapFrom((src, dest) =>
                {
                    HashSet<ComicTag> newTags = src.TagIds?
                        .Select(tagId => new ComicTag { TagId = IdParse(tagId), ComicId = dest.Id })
                        .ToHashSet() ?? [];
                    return newTags;
                });
            })
            .ForMember(dest => dest.PublisherId, options =>
            {
                options.Condition(src => src.PublisherId != null);
                options.MapFrom(src => IdParse(src.PublisherId));
            })
            .ForMember(dest => dest.Publisher, options => options.Ignore())
            .ForMember(dest => dest.Tags, options => options.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.TagIds,
                options => options.MapFrom(src => src.ComicTags.Select(tag => tag.TagId.ToString())));

        CreateMap<CommentModel, Comment>()
            .ForMember(dest => dest.AuthorId, options =>
            {
                options.Condition(src => src.AuthorId != null);
                options.MapFrom(src => IdParse(src.AuthorId));
            })
            .ForMember(dest => dest.ReplyToId, options =>
            {
                options.Condition(src => src.ReplyToId != null);
                options.MapFrom(src => IdParse(src.ReplyToId));
            });
        CreateMap<Comment, CommentModel>()
            .ForMember(dest => dest.AuthorId, options => options.MapFrom(src => src.AuthorId.ToString()))
            .ForMember(dest => dest.ReplyToId, options => options.MapFrom(src => src.ReplyToId.ToString()));

        CreateMap<ChapterCommentModel, ChapterComment>()
            .ForMember(dest => dest.ChapterId, options =>
            {
                options.Condition(src => src.ChapterId != null);
                options.MapFrom(src => IdParse(src.ChapterId));
            });
        CreateMap<ChapterComment, ChapterCommentModel>()
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => src.ChapterId.ToString()));

        CreateMap<ComicCommentModel, ComicComment>()
            .ForMember(dest => dest.ComicId, options =>
            {
                options.Condition(src => src.ComicId != null);
                options.MapFrom(src => IdParse(src.ComicId));
            });
        CreateMap<ComicComment, ComicCommentModel>()
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()));

        CreateMap<ProfileCommentModel, ProfileComment>()
            .ForMember(dest => dest.ProfileId, options =>
            {
                options.Condition(src => src.ProfileId != null);
                options.MapFrom(src => IdParse(src.ProfileId));
            });
        CreateMap<ProfileComment, ProfileCommentModel>()
            .ForMember(dest => dest.ProfileId, options => options.MapFrom(src => src.ProfileId.ToString()));

        CreateMap<HistoryRecordModel, HistoryRecord>()
            .ForMember(dest => dest.UserId, options =>
            {
                options.Condition(src => src.UserId != null);
                options.MapFrom(src => IdParse(src.UserId));
            })
            .ForMember(dest => dest.ChapterId, options =>
            {
                options.Condition(src => src.ChapterId != null);
                options.MapFrom(src => IdParse(src.ChapterId));
            });
        CreateMap<HistoryRecord, HistoryRecordModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => src.ChapterId.ToString()));

        CreateMap<LibraryCategoryModel, LibraryCategory>()
            .ForMember(dest => dest.UserId, options =>
            {
                options.Condition(src => src.UserId != null);
                options.MapFrom(src => IdParse(src.UserId));
            });

        CreateMap<LibraryCategory, LibraryCategoryModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()));

        CreateMap<LibraryEntryModel, LibraryEntry>()
            .ForMember(dest => dest.UserId, options =>
            {
                options.Condition(src => src.UserId != null);
                options.MapFrom(src => IdParse(src.UserId));
            })
            .ForMember(dest => dest.ComicId, options =>
            {
                options.Condition(src => src.ComicId != null);
                options.MapFrom(src => IdParse(src.ComicId));
            })
            .ForMember(dest => dest.Categories, options =>
            {
                options.Condition(src => src.CategoryIds != null && src.CategoryIds.Count != 0);
                options.MapFrom(src => src.CategoryIds!.Select(IdParse).ToHashSet());
            });
        CreateMap<LibraryEntry, LibraryEntryModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()))
            .ForMember(dest => dest.Categories, options => options.MapFrom(src => src.Categories.Select(category => category.Name.ToString()).ToArray()))
            .ForMember(dest => dest.CategoryIds, options => options.MapFrom(src => src.Categories.Select(category => category.Id.ToString()).ToArray()));

        CreateMap<TagModel, Tag>()
            .ForMember(dest => dest.CategoryId, options =>
            {
                options.Condition(src => src.CategoryId != null);
                options.MapFrom(src => IdParse(src.CategoryId));
            });

        CreateMap<Tag, TagModel>()
            .ForMember(dest => dest.Category, options => options.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.CategoryId, options => options.Ignore());

        CreateMap<User, ProfileModel>()
            .ForMember(dest => dest.Balance, options => options.Ignore());

        CreateMap<UserInputModel, User>();
        CreateMap<User, UserOutputModel>();
    }

    private static ulong IdParse(string? id)
    {
        return ulong.TryParse(id, out ulong result) ? result : 0;
    }
}