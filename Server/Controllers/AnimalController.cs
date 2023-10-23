using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using STGeneticsTechTestLeonardoMosquera.Server.Data;
using STGeneticsTechTestLeonardoMosquera.Shared.Models;

namespace STGeneticsTechTestLeonardoMosquera.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private DatabaseContext _ctx;

        public AnimalController(DatabaseContext ctx)
        {
            _ctx = ctx;
        }

        [HttpPost]
        public async Task<IActionResult> AddInitialDataIfEmpty()
        {
            if (!_ctx.Animal.Any())
            {
                var json = await System.IO.File.ReadAllTextAsync("../Shared/Models/animal.json");
                var initialData = JsonConvert.DeserializeObject<List<Animal>>(json);

                _ctx.Animal.AddRange(initialData);
                await _ctx.SaveChangesAsync();
            }

            return Ok();
        }

        [HttpPost]
        public IActionResult AddUpdate(Animal animal)
        {

            var status = new Status();
            if (!ModelState.IsValid)
            {
                status.StatusCode = 0;
                status.Message = "Invalid Data";
                return Ok(status);
            }
            try
            {
                if (animal.Id == 0)
                    _ctx.Animal.Add(animal);
                else
                    _ctx.Animal.Update(animal);
                _ctx.SaveChanges();
                status.StatusCode = 1;
                status.Message = $"{animal.Name} was saved successfully";


            }
            catch (Exception)
            {

                status.StatusCode = 0;
                status.Message = "Failed";
            }
            return Ok(status);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Status status = new Status();
            try
            {
                var animal = _ctx.Animal.Find(id);
                if (animal == null)
                {
                    status.StatusCode = 0;
                    status.Message = "Animal not found";
                    return Ok(status);
                }
                _ctx.Animal.Remove(animal);
                _ctx.SaveChanges();
                status.StatusCode = 1;
                status.Message = $"{animal.Name} was deleted success successfully";
            }
            catch (Exception)
            {

                status.StatusCode = 0;
                status.Message = "Failed";
            }
            return Ok(status);
        }

        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {

            var data = _ctx.Animal.Find(id);

            return Ok(data);

        }

        [HttpGet]
        public IActionResult GetAll(int pageNum = 1, string sTerm = "", int pageSize = 10, string sortOrder = "")
        {
            sTerm = sTerm.ToLower();
            var query = (
                from animal in _ctx.Animal
                where sTerm == null || animal.Name.ToLower().Contains(sTerm)
                || animal.Breed.ToLower().Contains(sTerm)
                || animal.BirthDate.ToString().Contains(sTerm)
                || animal.Price.ToString().Contains(sTerm)
                || animal.Sex.ToLower().Contains(sTerm)
                || animal.Status.ToLower().Contains(sTerm)
                select new Animal
                {
                    Id = animal.Id,
                    Name = animal.Name,
                    Breed = animal.Breed,
                    BirthDate = animal.BirthDate,
                    Sex = animal.Sex,
                    Price = animal.Price,
                    Status = animal.Status
                }
            );

            switch (sortOrder)
            {
                case "name_desc":
                    query = query.OrderByDescending(a => a.Name);
                    break;
                case "breed":
                    query = query.OrderBy(a => a.Breed);
                    break;
                case "breed_desc":
                    query = query.OrderByDescending(a => a.Breed);
                    break;
                case "birthdate":
                    query = query.OrderBy(a => a.BirthDate);
                    break;
                case "birthdate_desc":
                    query = query.OrderByDescending(a => a.BirthDate);
                    break;
                case "price":
                    query = query.OrderBy(a => a.Price);
                    break;
                case "price_desc":
                    query = query.OrderByDescending(a => a.Price);
                    break;
                case "sex":
                    query = query.OrderBy(a => a.Sex);
                    break;
                case "sex_desc":
                    query = query.OrderByDescending(a => a.Sex);
                    break;
                case "status":
                    query = query.OrderBy(a => a.Status);
                    break;
                case "status_desc":
                    query = query.OrderByDescending(a => a.Status);
                    break;
                default:
                    query = query.OrderBy(a => a.Name);
                    break;
            }

            var data = query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToList();

            int totalItems = query.Count();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            AnimalList model = new AnimalList
            {
                Animals = data,
                SearchTerm = sTerm,
                ToatalPages = totalPages,
                CurrentPage = pageNum
                
            };

            return Ok(model);
        }
    }
}
