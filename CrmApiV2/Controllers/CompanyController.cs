using CrmApiV2.Dtos.Company;
using CrmApiV2.Dtos.Response;
using CrmApiV2.Interface;
using CrmApiV2.Mapper;
using CrmApiV2.Models;
using CrmApiV2.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrmApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepo;
        public CompanyController(ICompanyRepository companyRepo)
        {
            _companyRepo = companyRepo;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var companyList = await _companyRepo.GetAllAsync();
            var companyDtoList = companyList.Select(x => x.ToCompanyDto()).ToList();
            var response = new ApiResponseDto<List<CompanyDto>>
            {
                Status = SD.Success,
                Message = "",
                Data = companyDtoList
            };
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = "Company not found"
                });
            }

            var companyDto = company.ToCompanyDto();
            var response = new ApiResponseDto<CompanyDto>
            {
                Status = SD.Success,
                Message = "",
                Data = companyDto
            };
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateCompanyDto companyCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = "Invalid data"
                });
            }

            try
            {
                var company = companyCreateDto.ToCompanyToCreateDto();
                var createdCompany = await _companyRepo.CreateAsync(company);
                var companyDto = createdCompany.ToCompanyDto();

                var response = new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Success,
                    Message = "Company created successfully",
                    Data = companyDto
                };
                return CreatedAtAction(nameof(GetById), new { id = createdCompany.Id }, response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = ex.Message
                });
            }
            
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] UpdateCompanyDto companyUpdateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = "Invalid data"
                });
            }
            try
            {
                var company = await _companyRepo.GetByIdAsync(id);
                if (company == null)
                {
                    return NotFound(new ApiResponseDto<CompanyDto>
                    {
                        Status = SD.Failure,
                        Message = "Company not found"
                    });
                }

                var updatedCompany = companyUpdateDto.ToCompanyToUpdateDto();
                updatedCompany.Id = id;

                var result = await _companyRepo.UpdateAsync(updatedCompany);
                var companyDto = result.ToCompanyDto();

                var response = new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Success,
                    Message = "Company updated successfully",
                    Data = companyDto
                };
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = ex.Message
                });
            }
            
        }

        
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var company = await _companyRepo.GetByIdAsync(id);
            if (company == null)
            {
                return NotFound(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = "Company not found"
                });
            }

            try
            {
                await _companyRepo.DeleteAsync(id);
                var response = new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Success,
                    Message = "Company deleted successfully"
                };
                return Ok(response);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new ApiResponseDto<CompanyDto>
                {
                    Status = SD.Failure,
                    Message = ex.Message
                });
            }
        }


    }
}
