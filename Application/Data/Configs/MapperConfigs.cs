using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yomikaze.Domain.Entities;
using Yomikaze.Domain.Entities.Identity;
using Yomikaze.Domain.Models.Common;

namespace Yomikaze.Application.Data.Configs;
public class MapperConfigs : Profile
{
    public MapperConfigs()
    {
        CreateMap<ChapterInputModel, Chapter>();
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
