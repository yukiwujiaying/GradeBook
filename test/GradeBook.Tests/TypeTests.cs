using System;
using Xunit;

 

namespace GradeBook.Tests
{
   
   public delegate string WiritelogDelegate(string logMessage);
    public class TypeTests
    {
        [Fact]
        public void WiritelogDelegateCanPointToMethod()
        {
            // same as
            //WiritelogDelegate log = new WiritelogDelegate(ReturnMessage);
            //or
            //WiritelogDelegate log = RetuenMessage;
            WiritelogDelegate log;
            log = new WiritelogDelegate(ReturnMessage);
            
            var result = log("Hello!");
            Assert.Equal("Hello!", result);
        }
        string ReturnMessage(string message)
        {
            return message;
        }
  
        [Fact]
        public void StringBehaveLikeValuesTypes()
        {
            string name = "Yuki";
            MakeUppercase(name); // still return yuki
            var name2 = MakeUppercase(name);// return YUKI

            // Toupper return only a copy (return a new string)
            Assert.Equal("Yuki", name);
            Assert.Equal("YUKI", name2);
        }
        private string MakeUppercase(string parameter)
        {
            return parameter.ToUpper();
        }
        [Fact]
        public void Test1()
        {
            var x = GetInt();
            SetInt(ref x);
            //without ref x
            //the below test will fail as we are copying it into the memory location
            //the x=3 is not effected 
            Assert.Equal(42,x);
        }

        private void SetInt(ref int z){
            z = 42;
        }

        private int GetInt()
        {
            return 3;
        }
        
        [Fact]
        public void CSharpCanPassByRef()
        {
           
            var book1 = GetBook("Book 1");
            //reference here
            GetBookSetName(ref book1,"new name");

            Assert.Equal("new name",book1.Name);
            
            
        }
        //ref means when the parameter arrive what i will recieve is not a copy of the value that is in the variable that i pass along,
        //Instead what i will get is a reference to the memory location of where that variable is stored
        //here for example the book1 is related to this method
        //compare with the below method without ref
        private void GetBookSetName(ref Book book, string name)
        {
            book =new Book(name);
        }


        [Fact]
        public void CSharpIsPassByValue()
        {
           
            var book1 = GetBook("Book 1");
            GetBookSetName(book1,"new name");

            Assert.Equal("Book 1",book1.Name);
            
            
        }

        // This is reference to a new book object which is not realted to book 1
        private void GetBookSetName(Book book, string name)
        {
            book =new Book(name);
        }


        [Fact]
        public void CanSetNameFromReference()
        {
           
            var book1 = GetBook("Book 1");
            SetName(book1,"new name");

            Assert.Equal("new name",book1.Name);
            
            
        }

        private void SetName(Book book, string newname)
        {
            book.Name= newname;
        }

        [Fact]
        public void GetBookReturnDifferentObjects()
        {
           
            var book1 = GetBook("Book 1");
            var book2 = GetBook("Book 2");

            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 2",book2.Name);
            
        }

        [Fact]
        public void TwoVarsCanReferenceSameObject()
        {
           
            var book1 = GetBook("Book 1");
            var book2 = book1;

            Assert.Equal("Book 1",book1.Name);
            Assert.Equal("Book 1",book2.Name);
            Assert.Same(book1, book2); 
            Assert.True(object.ReferenceEquals(book1,book2));
            
            
        }
        Book GetBook(string name)
        {
            return new Book(name);

        }
    }
}
