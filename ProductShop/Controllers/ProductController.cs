using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductShop.DAL;
using ProductShop.DAL.Entities;
using ProductShop.DTO.EntitiesDTO;
using ProductShop.DTO.ResultDTO;

namespace ProductShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly EFContext _context;


        public ProductController(EFContext c)
        {
            _context = c;
        }

        [HttpGet]
        public CollectionResultDTO<ProductDTO> GetProducts()
        {
            try
            {
                List<ProductDTO> films = new List<ProductDTO>();
                foreach (var film in _context.Products)
                {
                    ProductDTO filmDTO = new ProductDTO
                    {
                        Id = film.Id,
                        Name = film.Name,
                        Description = film.Description,
                        Image = film.Image,
                        Year = film.Year,
                        Category = film.Category
                    };
                    films.Add(filmDTO);

                }

                return new CollectionResultDTO<ProductDTO>
                {
                    StatusCode = true,
                    Result = films
                };

            }
            catch (Exception exception)
            {
                return new CollectionResultDTO<ProductDTO>
                {
                    StatusCode = false,
                    Message = exception.Message
                };

            }
        }

        [HttpPost]
        [Route("add")]
        public ResultDTO AddProduct([FromBody] AddProductDTO film)
        {
            try
            {
                Product newProduct = new Product
                {
                    Name = film.Name,
                    Description = film.Description,
                    Image = film.Image,
                    Year = film.Year,
                    Category = _context.Categories.FirstOrDefault(x => x.Name == film.CategoryName)

                };
                _context.Products.Add(newProduct);
                _context.SaveChanges();
                return new ResultDTO { StatusCode = true };

            }
            catch (Exception exception)
            {
                return new ResultDTO { StatusCode = false };
            }
        }

        [HttpGet]
        [Route("remove/{id}")]
        public ResultDTO RemoveProduct([FromRoute] int id)
        {
            try
            {
                Product film = _context.Products.FirstOrDefault(x => x.Id == id);
                if (film != null)
                {
                    _context.Products.Remove(film);
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
        public ResultDTO EditProduct(ProductDTO model)
        {
            try
            {
                Product film = new Product
                {
                    Id = model.Id,
                    Name = model.Name,
                    Category = _context.Categories.FirstOrDefault(x => x.Name == model.Category.Name),
                    Description = model.Description,
                    Image = model.Image,
                    Year = model.Year

                };

                _context.Products.Update(film);
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
        [HttpGet]
        [Route("getProduct/{id}")]
        public ResultDTO GetProduct([FromRoute] int id)
        {
            try
            {
                Product film = _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
                ProductDTO dto = new ProductDTO()
                {
                    Id = film.Id,
                    Name = film.Name,
                    Category = film.Category,
                    Description = film.Description,
                    Image = film.Image,
                    Year = film.Year
                };


                return new SingleResultDTO<ProductDTO>
                {
                    StatusCode = true,
                    Result = dto
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
