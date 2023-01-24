using Lab2._3.Models;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using static System.Net.Mime.MediaTypeNames;

namespace Lab2._3.Controllers
{
    public class MockupsController : Controller
    {
        private readonly ILogger<MockupsController> _logger;

        public MockupsController(ILogger<MockupsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Quiz()
        {
            TasksModel model= new TasksModel();
            model.generateTask();
            Tasks.Instance().add(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Quiz(string result, string action)
        {
            Tasks.Instance().getList().Last().result = result;
            Tasks.Instance().lastCheck();
            if (action == "Finish")
                return RedirectToAction("quizRes");

            TasksModel model = new TasksModel();
            model.generateTask();
            Tasks.Instance().add(model);    

            return View(model);
        }
        public IActionResult QuizRes()
        {
            return View(Tasks.Instance());
        }
    }
}