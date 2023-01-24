using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Diagnostics.Metrics;

namespace Lab2._3.Models
{
    public class TasksModel
    {
        public int first;
        public int second;
        public char sign;
        public string result { get; set; }

        public bool accuracy;
        public const int MAX = 20;
        public const int MIN = 0;

        public void generateTask()
        {
            Random random= new Random();
            first = random.Next(MIN, MAX);
            second = random.Next(MIN, MAX);
            int signRnd = random.Next(0, 3);
            switch (signRnd)
            {
                case 0:
                    sign = '+';
                    break;
                case 1:
                    sign = '-';
                    break;
                case 2:
                    sign = '*';
                    break;
                case 3:
                    sign =  '/';
                    break;
            }
            if(second == 0 && sign =='/')
                second = random.Next(MIN, MAX);
        }
        public bool check()
        {
            int temp;
            if (!int.TryParse(result, out temp)){
                accuracy= false;
                return false;
            }
            int answer = Int32.Parse(result);
            int correctAnswer = 0;
            switch (sign)
            {
                case '+':
                    correctAnswer = first+ second;
                    break;
                case '-':
                    correctAnswer = second-first;
                    break;
                case '*':
                    correctAnswer= first*second;
                    break;
                case '/':
                    correctAnswer= second/second;
                    break;
            }
            if(correctAnswer == answer)
                accuracy= true;
            else
                accuracy= false;
            return accuracy;
        }
    }

    public class Tasks
    {
        public int counterOfTasks = 0;
        public int counterCorrectAnswers = 0;
        private static Tasks list = new Tasks();
        public static ref Tasks Instance()
        {
            return ref list;
        }
        private Tasks() { }
        List<TasksModel> tasks = new List<TasksModel>();
        public void add(TasksModel task)
        {
            tasks.Add(task);
            counterOfTasks++;
        }
        public void lastCheck()
        {
            if (tasks.Last().check())
                counterCorrectAnswers++;
        }

        public List<TasksModel> getList() 
        {
            return tasks;
        }
        public void clear()
        {
            counterOfTasks = 0;
            counterCorrectAnswers = 0;

            tasks.Clear();
        }
    }
}