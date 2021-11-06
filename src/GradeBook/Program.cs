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
            book.GradeAdded += OnGradeAdded;
    

            


            var done = false;
            while (!done)
            {
                Console.WriteLine("Enter a grade or 'q' to quit");
                var input =Console.ReadLine();

                if (input == "q")
                {
                    done= true;
                    continue;
                }

                try
                {
                    var grade = double.Parse(input);
                    book.AddGrade(grade); 
                }
                //grade >100 or grade<0
                catch(ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);     
                }
                //grade is not a number
                catch(FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                //finally is the code you want to execute the try successfully and non successfully
                finally
                {
                    Console.WriteLine("**");
                }
            }
            
            
            
            var stats = book.GetStatistics();
            Console.WriteLine($"For the book named {book.Name}");
            Console.WriteLine($"The average grade is {stats.Average:N1}");  
            Console.WriteLine($"The hihest grade is {stats.High}");
            Console.WriteLine($"The lowest grade is {stats.Low}");
            Console.WriteLine($"The letter grade is {stats.letter}"); 
            
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("An grade is added.");
        }
    }
}
