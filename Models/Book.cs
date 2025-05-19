using System;



namespace Task.models
{

    public abstract class BaseModel
    {
        public int Id { get; set; }
    }

    public class Book : BaseModel
    {
        public string Name { get; set; }
        public string AuthorName { get; set; }
        public double Price { get; set; }

        public void ShowInfo()
        {
            Console.WriteLine(
                $"id: {Id} " +
                $"Name: {Name} " +
                $"AuthorName: {AuthorName} " +
                $"Price: {Price} "
            );
        }

        public override string ToString()
        {
            return $"id: {Id} Name: {Name} AuthorName: {AuthorName} Price: {Price}";
        }
    }
}