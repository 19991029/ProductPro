using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductPro.Data;
using ProductPro.logging;
using ProductPro.Models;
using ProductPro.Models.Dto;
using ProductPro.Repository;

namespace ProductPro.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductApiController : ControllerBase
    {
       // private readonly ILogging Logger;
        //private readonly ProductDbContext db;
        private readonly IMapper mapper;
        private readonly IProductReposotory repo;



        //public ProductApiController(ILogger<ProductApiController> _logger) 
        //{
        //    Logger = _logger;
        //}
        public ProductApiController(IProductReposotory _repo, IMapper _mapper)
        {
            //   Logger = _logging;
            //db = _Db;
            repo = _repo;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
        {
            IEnumerable<Product> productList = await repo.GetAll();
            //Logger.Log("GetAllProducts","");

            return Ok(mapper.Map<List<ProductDto>>(productList));
        }

        [HttpGet("{id:int}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(200)]
        public ActionResult<ProductDto> GetProduct(int id)
        {
            if (id == 0)
            {
             //   Logger.Log("GetProduct" + id, "error");
                return BadRequest();
            }
            var Product = repo.Get(p => p.Id == id);
            if (Product == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ProductDto>(Product));


        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductDto>> CreateProduct([FromBody] ProductCreateDto productDto)
        {

            if (productDto == null)
            {
                return BadRequest();
            }

            if ((await repo.Get(p => p.Name == productDto.Name))!=null)

            {
                ModelState.AddModelError("CustomError", "Product already exist");
                return BadRequest(ModelState);
            }
            //if (productDto.Id > 0) 
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);

            //}

            //int maxId = db.Products.OrderByDescending(p => p.Id).FirstOrDefault().Id;
            //productDto.Id = maxId+1;
            //Product model = new Product()
            //{
            //    Name = productDto.Name
            //};
            Product model = mapper.Map<Product>(productDto);
            await repo.Create(model);
           
            //var productdto = new ProductDto 
            //{
            //    Id=model.Id, 
            //    Name=model.Name 
            //};
            var productdto = mapper.Map<ProductDto>(model);
            return CreatedAtRoute("GetProduct", new { id = productdto.Id }, productdto);

        }
        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var deleteproduct = await repo.Get(p => p.Id == id);
            if (deleteproduct == null)
            {
                return NotFound();
            }
            repo.Remove(deleteproduct);
           
            return NoContent();


        }
        [HttpPut("{id:int}", Name = "UpdateProduct")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDto productDto)
        {
            if (id == 0 || productDto == null)
            {
                return BadRequest();
            }

            var existingProduct = await repo.Get(p => p.Id == id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if ((await repo.Get(p => p.Id != id && p.Name == productDto.Name))!=null)
            {
                ModelState.AddModelError("CustomError", "Product name already exists for another product.");
                return BadRequest(ModelState);
            }
            //Product modal = new Product()
            //{
            //    Id = existingProduct.Id,
            //    Name = productDto.Name,
            //    Detail = existingProduct.Detail,
            //    Qty = existingProduct.Qty
            //};
            //Product modal = mapper.Map<Product>(existingProduct);
            existingProduct.Name = productDto.Name;
            // existingProduct.Name = updatedProductDto.Name;
           repo.Update(existingProduct);
           
            // You can update other properties as needed.

            return NoContent();

        }



    }
}