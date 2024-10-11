namespace StudentTeacherQnAPlatform.ViewModels
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }

    public class AnswerViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TeacherName { get; set; }
    }
    public class AnswerQuestionViewModel
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class ModerationQuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class TeacherAnswerViewModel
    {
        public int AnswerId { get; set; }
        public string AnswerContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public string QuestionTitle { get; set; }
    }
    public class UserQuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class QuestionDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
        public List<AnswerViewModel> Answers { get; set; } = new List<AnswerViewModel>(); 
    }

    public class AnswerViewModelDetails
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string TeacherName { get; set; }
    }
    public class RecentQuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
    }
    public class UnansweredQuestionViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UserName { get; set; }
    }

}
