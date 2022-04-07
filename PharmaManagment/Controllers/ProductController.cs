using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PharmaManagment.Models;
using PharmaManagment.Models.Dtos;
using PharmaManagment.Repository;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PharmaManagment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPharmaRepository _repository;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;

        public ProductController(IPharmaRepository repository, ILogger<ProductController> logger,IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(List<ProductDto>),(int)StatusCodes.Status200OK)]
        public async Task<ActionResult<List<ProductDto>>> GetProducts()
        {
            var prodcuts = await _repository.GetAllProducts();
            if (prodcuts == null)
                return Ok(new List<ProductDto>());
            var productsDto=_mapper.Map< List<ProductDto>>(prodcuts);
            return Ok(productsDto);
        }

        [HttpGet("{Id}",Name ="GetProductById")]
        [ProducesResponseType(typeof(ProductDto), (int)StatusCodes.Status200OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Products>> GetProductById(int Id)
        {
            var product=await _repository.GetProductsById(Id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        
    }
}
