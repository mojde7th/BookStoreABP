using Acme.BookStore.DTOs;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acme.BookStore.Entities;

namespace Acme.BookStore
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            CreateMap<CreateUpdateAuthorDto, Author>();
            CreateMap<Author, AuthorDto>();
                }
    }
}
