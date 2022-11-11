using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var numberOfStudents = Students.Count;
            if (numberOfStudents < 5)
            {
                throw new InvalidOperationException("Ranked-grading requires a minimum of 5 students to work");
            }

            var top20PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take(numberOfStudents / 5).LastOrDefault();
            if (averageGrade >= top20PercentLevel)
            {
                return 'A';
            }
            
            var top40PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take((int)(numberOfStudents * 0.4)).LastOrDefault();
            if (averageGrade >= top40PercentLevel)
            {
                return 'B';
            }
            
            var top60PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take((int)(numberOfStudents * 0.6)).LastOrDefault();
            if (averageGrade >= top60PercentLevel)
            {
                return 'C';
            }
            
            var top80PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take((int)(numberOfStudents * 0.8)).LastOrDefault();
            if (averageGrade >= top80PercentLevel)
            {
                return 'D';
            }
            
            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count(x => x.Grades.Count > 0) < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            
            base.CalculateStudentStatistics(name);
        }
    }
}