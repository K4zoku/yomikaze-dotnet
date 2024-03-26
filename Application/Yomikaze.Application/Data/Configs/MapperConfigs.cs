using AutoMapper;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Identity.Entities;
using Yomikaze.Domain.Models;

namespace Yomikaze.Application.Data.Configs;

public class MapperConfigs : Profile
{
    public MapperConfigs()
    {
        CreateMap<ChapterInputModel, Chapter>()
            .ForMember(dest => dest.Pages, options => options.Condition(src => src.Pages?.Count > 0));
        var map = CreateMap<ChapterIndexInputModel, Chapter>();
        map.ForAllMembers(opt => opt.Ignore());
        map.ForMember(dest => dest.Index, options => options.MapFrom(src => src.Index));
        
        CreateMap<Chapter, ChapterOutputModel>();

        CreateMap<ComicInputModel, Comic>();
        CreateMap<Comic, ComicOutputModel>();

        CreateMap<CommentInputModel, Comment>();
        CreateMap<Comment, CommentOutputModel>();

        CreateMap<GenreInputModel, Genre>();
        CreateMap<Genre, GenreOutputModel>();

        CreateMap<HistoryRecordInputModel, HistoryRecord>();
        CreateMap<HistoryRecord, HistoryRecordOutputModel>();

        CreateMap<LibraryEntryInputModel, LibraryEntry>();
        CreateMap<LibraryEntry, LibraryEntryOutputModel>();

        CreateMap<PageInputModel, Page>();
        CreateMap<Page, PageOutputModel>();

        CreateMap<UserInputModel, User>();
        CreateMap<User, UserOutputModel>();
    }
}