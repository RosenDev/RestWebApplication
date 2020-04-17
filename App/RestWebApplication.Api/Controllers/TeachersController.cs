using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using RestWebApplication.Api.Helpers.Enums;
using RestWebApplication.Common;
using RestWebApplication.Infrastructure.Helpers;
using RestWebApplication.Infrastructure.ResourceParameters;
using RestWebApplication.Services.Data.Contracts;
using RestWebApplication.Services.Models;

namespace RestWebApplication.Api.Controllers
{
    public class TeachersController : ApiController
    {

        private readonly ITeachersService teachersService;

        public TeachersController(ITeachersService teachersService)
        {
            this.teachersService = teachersService;
        }
        
        [HttpGet(Name = "GetTeachers")]
        [HttpHead]
        public IActionResult GetTeachers([FromQuery]TeachersResourceParameters resourceParameters)
        {

            ThrowHelper.ThrowIfNull(resourceParameters,nameof(resourceParameters));

            var teachers = teachersService
                .GetAll(resourceParameters);
            
            if (teachers==null)
            {
                return NoContent();
            }

            var previousPageLink = teachers.HasPrevious
                ?CreateTeachersResourceUri(resourceParameters,
                    ResourceUriType.PreviousPage).LowerCaseLink()
                : null;

            var nextPageLink = teachers.HasNext
                ? CreateTeachersResourceUri(resourceParameters,
                    ResourceUriType.NextPage).LowerCaseLink()
                : null;

            var paginationData = new
            {
                totalCount = teachers.TotalCount,
                pageSize = teachers.PageSize,
                currentPage = teachers.CurrentPage,
                totalPages = teachers.TotalPages,
                previousPageLink,
                nextPageLink
            };
            
            Response.Headers.Add("X-Pagination",
              JsonSerializer.Serialize(paginationData));
            
            return Ok(teachers.ShapeData(resourceParameters.Fields));
        }
        
        [HttpGet("{id:guid}",Name = "GetTeacher")]
        public async Task<IActionResult> GetTeacher(string id)
        {
            var teacher = await teachersService.GetAsync(id);
            if (teacher==null)
            {
                return NotFound();
            }
            return Ok(teacher);
        }
        
        [HttpPost(Name = "PostTeacher")]
        public async Task<ActionResult> PostTeacher([FromBody]TeacherDto teacherDto)
        {
            if (teacherDto==null)
            {
                return BadRequest("Teacher is null!");
            }
            var result =  await teachersService.CreateAsync(teacherDto);

            return Created(new Uri($"https://localhost:44312/api/teachers/{result}"),null);
        }
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put([FromRoute]string id,[FromBody]TeacherDto teacher)
        {
            teacher.Id = id;
            var result = await teachersService.UpdateAsync(teacher);

            return CreatedAtRoute("GetTeacher", new
            {
                id=result
            });
        }
        
        [HttpDelete("{id:guid}",Name = "DeleteTeacher")]
        public async Task<ActionResult> DeleteTeacher(string id)
        {
            var success = await teachersService.RemoveAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }

        private string CreateTeachersResourceUri(TeachersResourceParameters teachersResourceParameters,
            ResourceUriType resourceUriType)
        {
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetTeachers",
                        new
                        {
                            pageNumber=teachersResourceParameters.PageNumber-1,
                            pageSize=teachersResourceParameters.PageSize
                            //todo add search query 
                        });
                case ResourceUriType.NextPage:
                    return Url.Link("GetTeachers",
                        new
                        {
                            pageNumber=teachersResourceParameters.PageNumber+1,
                            pageSize=teachersResourceParameters.PageSize
                            //todo add search query 
                        });
                default:
                    return Url.Link("GetTeachers",
                        new
                        {
                            pageNumber=teachersResourceParameters.PageNumber,
                            pageSize=teachersResourceParameters.PageSize
                            //todo add search query 
                        });
                    
            }
        }
    }
}