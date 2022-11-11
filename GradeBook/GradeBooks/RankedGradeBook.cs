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
            if (averageGrade > top20PercentLevel)
            {
                return 'A';
            }
            
            var top40PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take(numberOfStudents / (2/5)).LastOrDefault();
            if (averageGrade > top40PercentLevel)
            {
                return 'B';
            }
            
            var top60PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take(numberOfStudents / (3/5)).LastOrDefault();
            if (averageGrade > top60PercentLevel)
            {
                return 'C';
            }
            
            var top80PercentLevel = Students.Select(x => x.AverageGrade).OrderByDescending(y => y)
                .Take(numberOfStudents / (4/5)).LastOrDefault();
            if (averageGrade > top60PercentLevel)
            {
                return 'D';
            }
            
            return 'F';
        }
    }
}