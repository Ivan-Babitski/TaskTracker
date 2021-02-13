using AutoMapper;
using System;
using System.Collections;
using System.Collections.Generic;
using TaskTracker.Application.Models;
using TaskTracker.Core.Models;

namespace TaskTracker.Application.Mapper
{
    // following solution run only once and be completely isolated in class library
    public class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // this line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<AspnetRunDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
        
        public class AspnetRunDtoMapper : Profile
        {
            public AspnetRunDtoMapper()
            {
                //CreateMap<Product, ProductModel>()
                //    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

                //CreateMap<FROM, TO>().ReverseMap();
                CreateMap<File, FileDto>().ReverseMap();
                CreateMap<Priority, PriorityDto>().ReverseMap();
                CreateMap<Task, TaskDto>().ReverseMap();

                CreateMap<Tag, TagDto>().ReverseMap();
                CreateMap<TaskTag, TaskTagDto>().ReverseMap();
                CreateMap<Tag, TagDto>().ReverseMap();
                CreateMap<User, UserDto>().ReverseMap();
            }
        }
    }
}