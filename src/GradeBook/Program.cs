using System;
using System.Collections.Generic;

namespace GradeBook
{
    
    class Program
    {
        
        static void Main(string[] args)
        //string is type, args is argument name of property
        {
           
            var book= new Book("yuki grade book");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.5);
            
            var stats = book.GetStatistics();
            Console.WriteLine($"The average grade is {stats.Average:N1}");  
            Console.WriteLine($"The hihest grade is {stats.High}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
           

           
            
            
        }
    }
}
