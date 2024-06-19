using Yomikaze.Domain.Abstracts;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Domain.Identity.Models;
using Yomikaze.Domain.Models;
using MapperProfile = AutoMapper.Profile;

namespace Yomikaze.Application.Data.Configs;

public class YomikazeMapper : MapperProfile
{
    private static ulong IdParse(string? id)
    {
        return ulong.TryParse(id, out ulong result) ? result : 0;
    }
    public YomikazeMapper()
    {
        CreateMap<BaseModel, BaseEntity>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => IdParse(src.Id)))
            .ForAllMembers(options => options.Condition((_, _, srcMember) => srcMember != null));
        
        CreateMap<BaseEntity, BaseModel>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.Id.ToString()));
        
        CreateMap<ChapterModel, Chapter>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.Pages, options =>
            {
                options.Condition(src => src.Pages is { Count: > 0 });
                options.MapFrom(src => (src.Pages  ?? new List<string>()).Select(page => new Page { Image = page }));
            })
            .ForMember(dest => dest.ComicId, options =>
            {
                options.Condition(src => src.ComicId != null);
                options.MapFrom(src => IdParse(src.ComicId));
            });
        CreateMap<Chapter, ChapterModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.Pages, options => options.MapFrom(src => src.Pages.Select(page => page.Image)))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()));

        CreateMap<CoinPricingModel, CoinPricing>().ReverseMap();

        CreateMap<ComicModel, Comic>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.ComicTags, options =>
            {
                options.Condition(src => src.TagIds is { Count: > 0 });
                options.MapFrom(src => (src.TagIds ?? new List<string>()).Select(id => new ComicTag { TagId = IdParse(id) }));
            });
        CreateMap<Comic, ComicModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.TagIds, options => options.MapFrom(src => src.ComicTags.Select(tag => tag.TagId.ToString())));

        CreateMap<CommentModel, Comment>()
            .IncludeBase<BaseModel, BaseEntity>()
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
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.AuthorId, options => options.MapFrom(src => src.AuthorId.ToString()))
            .ForMember(dest => dest.ReplyToId, options => options.MapFrom(src => src.ReplyToId.ToString()));

        CreateMap<ChapterCommentModel, ChapterComment>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.ChapterId, options =>
            {
                options.Condition(src => src.ChapterId != null);
                options.MapFrom(src => IdParse(src.ChapterId));
            });
        CreateMap<ChapterComment, ChapterCommentModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => src.ChapterId.ToString()));
        
        CreateMap<ComicCommentModel, ComicComment>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.ComicId, options =>
            {
                options.Condition(src => src.ComicId != null);
                options.MapFrom(src => IdParse(src.ComicId));
            });
        CreateMap<ComicComment, ComicCommentModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()));
        
        CreateMap<ProfileCommentModel, ProfileComment>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.ProfileId, options =>
            {
                options.Condition(src => src.ProfileId != null);
                options.MapFrom(src => IdParse(src.ProfileId));
            });
        CreateMap<ProfileComment, ProfileCommentModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.ProfileId, options => options.MapFrom(src => src.ProfileId.ToString()));

        CreateMap<HistoryRecordModel, HistoryRecord>()
            .IncludeBase<BaseModel, BaseEntity>()
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
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => src.ChapterId.ToString()));
        
        CreateMap<LibraryCategoryModel, LibraryCategory>()
            .IncludeBase<BaseModel, BaseEntity>()
            .ForMember(dest => dest.UserId, options =>
            {
                options.Condition(src => src.UserId != null);
                options.MapFrom(src => IdParse(src.UserId));
            });
        
        CreateMap<LibraryCategory, LibraryCategoryModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()));  
        
        CreateMap<LibraryEntryModel, LibraryEntry>()
            .IncludeBase<BaseModel, BaseEntity>()
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
            .ForMember(dest => dest.CategoryId, options =>
            {
                options.Condition(src => src.CategoryId != null);
                options.MapFrom(src => IdParse(src.CategoryId));
            });
        CreateMap<LibraryEntry, LibraryEntryModel>()
            .IncludeBase<BaseEntity, BaseModel>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.UserId.ToString()))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => src.ComicId.ToString()))
            .ForMember(dest => dest.CategoryId, options => options.MapFrom(src => src.CategoryId.ToString()));
        
        

        CreateMap<UserInputModel, User>();
        CreateMap<User, UserOutputModel>();
    }
}