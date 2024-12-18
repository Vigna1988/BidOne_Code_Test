using Form_test_application.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Xml;

namespace Form_test_application.Controllers
{
    public class PersonController : Controller
    {
        private readonly string _filePath = "wwwroot/data/persons.json";

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Save(Person person)
        {
            if (ModelState.IsValid)
            {
                // Read existing data
                List<Person> persons = new List<Person>();
                if (System.IO.File.Exists(_filePath))
                {
                    var existingData = System.IO.File.ReadAllText(_filePath);
                    persons = JsonConvert.DeserializeObject<List<Person>>(existingData) ?? new List<Person>();
                }

                // Add new person
                persons.Add(person);

                // Write to JSON file
                var jsonData = JsonConvert.SerializeObject(persons, Newtonsoft.Json.Formatting.Indented);
                System.IO.File.WriteAllText(_filePath, jsonData);

                ViewBag.Message = "Person saved successfully!";
            }
            return View("Index");
        }
    }
}
