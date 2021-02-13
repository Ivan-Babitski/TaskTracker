using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using TaskTracker.Application.Enums;
using TaskTracker.Application.Models;
using TaskTracker.UI.MVC.Models;

namespace TaskTracker.UI.MVC.Mapper
{
    public class MapperMVC : Profile
    {
        public MapperMVC()
        {
            //CreateMap<ExampleDto, ExampleViewModel>().ReverseMap();

            CreateMap<PriorityDto, SelectListItem>()
                .ForMember(tsk => tsk.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(tsk => tsk.Text, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            CreateMap<TaskCreateViewModel, TaskDto>()
                .ReverseMap();      
            
            CreateMap<TaskViewModel, TaskDto>()
                .ReverseMap();        
            
            CreateMap<TagModel, TagDto>()
                .ReverseMap();         
            
            CreateMap<UserModel, UserDto>()
                .ReverseMap();         
            
            CreateMap<FileModel, FileDto>()
                .ForMember(tsk => tsk.Name, opt => opt.MapFrom(src => src.FileName))
                .ReverseMap();         
            
            CreateMap<TaskModel, TaskDto>()
                .ReverseMap();

            CreateMap<UserModel, SelectListItem>()
                .ForMember(tsk => tsk.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(tsk => tsk.Text, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();            
            
            CreateMap<TaskDto, SelectListItem>()
                .ForMember(tsk => tsk.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(tsk => tsk.Text, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();        
            
            CreateMap<TaskViewModel, SelectListItem>()
                .ForMember(tsk => tsk.Value, opt => opt.MapFrom(src => src.Id))
                .ForMember(tsk => tsk.Text, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();

            //CreateMap<string, TagDto>()
            //    .ForMember(tsk => tsk.Name, opt => opt.MapFrom(src => src))
            //    .ReverseMap();  
        }
    }
}