using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StudentTeacherQnAPlatform.Entities;
using StudentTeacherQnAPlatform.Entities.Security;
using StudentTeacherQnAPlatform.Repositories;
using StudentTeacherQnAPlatform.Repositories.IRepository;
using StudentTeacherQnAPlatform.ViewModels;

namespace StudentTeacherQnAPlatform.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IQuestionService _questionService;

        public AccountController(IUserService userService, IQuestionService questionService)
        {
            _userService = userService;
            _questionService = questionService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    RoleId = model.RoleId,
                    Institute = model.Institute,
                    IDCardNumber = model.IDCardNumber
                };

                await _userService.RegisterUserAsync(user, model.Password);
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            if (user != null)
            {
                var result = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
                if (result == PasswordVerificationResult.Success)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();            
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult TeacherDashboard()
        {
            var unansweredQuestions = _questionService.GetUnansweredQuestions();
            return View(unansweredQuestions);
        }

        [HttpGet]
        public IActionResult ModeratorDashboard()
        {
            var questionsToModerate = _questionService.GetQuestionsToModerate();
            return View(questionsToModerate);
        }

        public IActionResult StudentDashboard()
        {
            var questions = _questionService.GetRecentQuestions();
            return View(questions);
        }

        [HttpPost]
        public async Task<IActionResult> PostQuestion(string title, string content)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var question = new Question
            {
                Title = title,
                Content = content,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _questionService.AddQuestionAsync(question);
            return RedirectToAction("StudentDashboard");
        }

        public IActionResult MyQuestions()
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var questions = _questionService.GetQuestionsByUserId(userId);
            return View(questions);
        }

        [HttpGet]
        public IActionResult AnswerQuestion(int id)
        {
            var question = _questionService.GetQuestionById(id);
            return View(question);
        }

        public IActionResult MyAnswers()
        {
            var teacherId = int.Parse(HttpContext.Session.GetString("UserId"));
            var answers = _questionService.GetAnswersByTeacherId(teacherId);
            return View(answers);
        }

        public IActionResult ModerationPanel()
        {
            var allQuestions = _questionService.GetAllQuestions();
            return View(allQuestions);
        }

        public async Task<IActionResult> RemoveQuestion(int id)
        {
            await _questionService.RemoveQuestionAsync(id);
            return RedirectToAction("ModerationPanel");
        }

        [HttpGet]
        public JsonResult GetRecentQuestions()
        {
            var questions = _questionService.GetRecentQuestions();
            return Json(questions);
        }

        public IActionResult AllQuestions()
        {
            var questions = _questionService.GetAllQuestions();
            return View(questions);
        }

        public IActionResult AskQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(string title, string content)
        {
            var userId = int.Parse(HttpContext.Session.GetString("UserId"));

            var question = new Question
            {
                Title = title,
                Content = content,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _questionService.AddQuestionAsync(question);

            return RedirectToAction("AllQuestions");
        }

        public async Task<IActionResult> QuestionDetails(int id)
        {
            var question = await _questionService.GetQuestionDetailsAsync(id);

            if (question == null)
            {
                return NotFound();
            }

            return View(question);
        }

        //[HttpPost]
        //public async Task<IActionResult> SubmitAnswer(int questionId, string content)
        //{
        //    var userIdString = HttpContext.Session.GetString("UserId");

        //    if (string.IsNullOrEmpty(userIdString))
        //    {
        //        return BadRequest("You must be logged in to submit an answer.");
        //    }

        //    int userId = int.Parse(userIdString);

        //    var answer = new Answer
        //    {
        //        Content = content,
        //        QuestionId = questionId,
        //        TeacherId = userId,
        //        CreatedDate = DateTime.UtcNow
        //    };

        //    await _questionService.AddAnswerAsync(answer);

        //    return Ok();
        //}

        [HttpPost]
        public async Task<IActionResult> SubmitAnswer(int questionId, string content)
        {
            var userIdString = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrEmpty(userIdString))
            {
                return BadRequest("You must be logged in to submit an answer.");
            }

            int userId = int.Parse(userIdString);

            var answer = new Answer
            {
                Content = content,
                QuestionId = questionId,
                TeacherId = userId,
                CreatedDate = DateTime.UtcNow
            };

            await _questionService.AddAnswerAsync(answer);
            var teacher = await _questionService.GetUserByIdAsync(userId);
            var teacherName = teacher?.Name ?? "Unknown";
            return Json(new
            {
                teacherName = teacherName,
                answerContent = answer.Content,
                createdDate = answer.CreatedDate.ToString("yyyy-MM-dd HH:mm")
            });
        }
    }
}
