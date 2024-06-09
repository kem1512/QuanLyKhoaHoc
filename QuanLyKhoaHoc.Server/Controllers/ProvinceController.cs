﻿using Microsoft.AspNetCore.Mvc;
using QuanLyKhoaHoc.Application;
using QuanLyKhoaHoc.Application.Common.Mappings;

namespace QuanLyKhoaHoc.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinceController : BaseController<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate>
    {
        public ProvinceController(ApplicationServiceBase<ProvinceMapping, ProvinceQuery, ProvinceCreate, ProvinceUpdate> context)
            : base(context)
        {
        }
    }
}