using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductShop.DAL;
using ProductShop.DAL.Entities;
using ProductShop.DTO.EntitiesDTO;
using ProductShop.DTO.ResultDTO;

namespace ProductShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly EFContext _context;


        public CategoryController(EFContext c)
        {
            _context = c;
        }

        [HttpGet]
        public CollectionResultDTO<CategoryDTO> GetCategories()
        {
            try
            {
                List<CategoryDTO> categories = new List<CategoryDTO>();
                foreach (var category in _context.Categories)
                {
                    CategoryDTO categoryDTO = new CategoryDTO
                    {
                        Id = category.Id,
                        Name = category.Name,

                    };
                    categories.Add(categoryDTO);

                }

                return new CollectionResultDTO<CategoryDTO>
                {
                    StatusCode = true,
                    Result = categories
                };

            }
            catch (Exception exception)
            {
                return new CollectionResultDTO<CategoryDTO>
                {
                    StatusCode = false,
                    Message = exception.Message
                };

            }
        }

        [HttpPost]
        [Route("add")]
        public ResultDTO AddCategory([FromBody] CategoryDTO category)
        {
            try
            {
                if (_context.Categories.FirstOrDefault(x => x.Name == category.Name) == null)
                {
                    Category newCategory = new Category
                    {
                        Name = category.Name
                    };
                    _context.Categories.Add(newCategory);
                    _context.SaveChanges();
                    return new ResultDTO
                    {
                        StatusCode = true
                    };
                }
                else
                {
                    return new ResultDTO
                    {
                        StatusCode = false,
                        Message = "False"
                    };

                }

            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    StatusCode = false,
                    Message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("remove/{id}")]
        public ResultDTO RemoveCategory([FromRoute] int id)
        {
            try
            {
                Category category = _context.Categories.FirstOrDefault(x => x.Id == id);
                if (category != null)
                {
                    _context.Categories.Remove(category);
                    _context.SaveChanges();
                    return new ResultDTO { StatusCode = true };
                }
                else
                {
                    return new ResultDTO { StatusCode = false, Message = "wrong" };
                }
            }
            catch (Exception ex)
            {
                return new ResultDTO { StatusCode = false, Message = "wrong" };
            }
        }

        [HttpPost("edit")]
        public ResultDTO EditCategory(CategoryDTO model)
        {
            try
            {
                Category category = new Category
                {
                    Id = model.Id,
                    Name = model.Name

                };

                _context.Categories.Update(category);
                _context.SaveChanges();

                return new ResultDTO
                {
                    StatusCode = true
                };
            }
            catch (Exception ex)
            {
                return new ResultDTO
                {
                    StatusCode = false,
                    Message = ex.Message
                };
            }
        }
    }
}
