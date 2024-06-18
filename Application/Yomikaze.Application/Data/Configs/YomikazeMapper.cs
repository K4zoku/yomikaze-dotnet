using AutoMapper;
using Microsoft.Extensions.Options;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Weak;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Domain.Identity.Models;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Data.Configs;

public class YomikazeMapper : Profile
{
    public YomikazeMapper()
    {
        CreateMap<ChapterInputModel, Chapter>()
            .ForMember(dest => dest.Pages, options => options.Condition(src => src.Pages?.Count > 0))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => ulong.Parse(src.ComicId)));
        CreateMap<ChapterIndexInputModel, Chapter>()
            .ForMember(dest => dest.Index, options => options.MapFrom(src => src.Index))
            .ForAllMembers(opt => opt.Ignore());
        CreateMap<Chapter, ChapterOutputModel>();

        CreateMap<ComicInputModel, Comic>();
        CreateMap<Comic, ComicOutputModel>();

        CreateMap<CommentInputModel, Comment>()
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => ulong.Parse(src.ComicId)))
            .ForMember(dest => dest.ReplyToId, options => options.MapFrom(src => src.ReplyToId == null ? default : ulong.Parse(src.ReplyToId)));
        CreateMap<Comment, CommentOutputModel>();

        CreateMap<TagInputModel, Tag>();
        CreateMap<Tag, TagOutputModel>();

        CreateMap<HistoryRecordInputModel, HistoryRecord>()
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => ulong.Parse(src.ChapterId)))
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => ulong.Parse(src.UserId)));
        CreateMap<HistoryRecord, HistoryRecordOutputModel>();

        CreateMap<LibraryEntryInputModel, LibraryEntry>()
            .ForMember(dest => dest.UserId, options => options.MapFrom(src => ulong.Parse(src.UserId)))
            .ForMember(dest => dest.ComicId, options => options.MapFrom(src => ulong.Parse(src.ComicId)));
        CreateMap<LibraryEntry, LibraryEntryOutputModel>();

        CreateMap<PageInputModel, Page>()
            .ForMember(dest => dest.ChapterId, options => options.MapFrom(src => ulong.Parse(src.ChapterId)));
        CreateMap<Page, PageOutputModel>();

        CreateMap<UserInputModel, User>();
        CreateMap<User, UserOutputModel>();
    }
}