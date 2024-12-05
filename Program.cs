using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using static System.Console;
namespace OOP_Progect_Library
{
    class Book
    {
        public readonly string Title;
        public readonly string Author;
        public readonly string Subject;
        public bool IsAvailable;
        public Book(string Title, string Author, string Subject)
        {
            this.Title = Title;
            this.Author = Author;
            this.Subject = Subject;
            IsAvailable = true;
        }
    }
    class Library
    {
        public List<Book> Books = new List<Book>();
        public Library()
        {
            Books = new List<Book>
            {
            new Book("Origins", "Lewis Dartnell", "History"),
            new Book("1491", "Charles C. Mann", "History"),
            new Book("Sapiens", "Yuval Noah Harari", "History"),
            new Book("Cosmos", "Carl Sagan", "Physics"),
            new Book("Light", "Albert A. Michelson", "Physics"),
            new Book("Quantum", "Manjit Kumar", "Physics"),
            new Book("Animal Farm", "George Orwell", "Novels"),
            new Book("Dracula", "Bram Stoker", "Novels"),
            new Book("Frankenstein", "Mary Shelley", "Novels"),
            new Book("Ethics", "Baruch Spinoza", "Philosophy"),
            new Book("Being", "Martin Heidegger", "Philosophy"),
            new Book("Existence", "Jean-Paul Sartre", "Philosophy") ,
            new Book("test1","test2","History"),
            new Book("test1","test2","Novels"),
            new Book("test4","test3","Philosophy"),
            new Book("test4","test3","Uncategorized")
            };
        }
        //search method
        public List<Book> Search(string Title, string Author)
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Author))
                throw new Exception("Author name or book title cannot be blank.");
            List<Book> temp_Books = (from book in Books
                                     where book.Title == Title && book.Author == Author
                                     select book).ToList();
            return temp_Books;
        }
        //We will use this method if the user doesn't know whether what they know is the title or the author's name
        public (List<Book>, List<Book>) SearchByBoth(string TitleOrAuthor)
        {
            if (string.IsNullOrWhiteSpace(TitleOrAuthor))
                throw new Exception("there is no book whose author's name or title are nothing!!!!!");

            List<Book> MatchTheAuthorsName = new List<Book>();
            List<Book> MatchTheTitle = new List<Book>();

            for (int i = 0; i < Books.Count; i++)
            {
                if (Books[i].Title == TitleOrAuthor)
                    MatchTheTitle.Add(Books[i]);
                if (Books[i].Author == TitleOrAuthor)
                    MatchTheAuthorsName.Add(Books[i]);
            }

            return (MatchTheAuthorsName, MatchTheTitle);
        }
        public void Add(Book Added_Book)
        {
            if (string.IsNullOrWhiteSpace(Added_Book.Title) || string.IsNullOrWhiteSpace(Added_Book.Author))
            {
                throw new Exception("the book must have a title and author name");
            }
            for (int i = 0; i < Books.Count; i++)
                if (Books[i].Author == Added_Book.Author && Books[i].Title == Added_Book.Title && Books[i].Subject == Added_Book.Subject)
                {
                    WriteLine("the book is already in the library.");
                    return;
                }
            WriteLine("The book has been added.\n--------------------");
            Books.Add(Added_Book);
        }
        //Show random books to user
        public void DisPlayRandomBook()
        {
            int limit;
            if (Books.Count == 0)
            { WriteLine("there is no book in this library!."); return; }
            else if (Books.Count < 4)
                limit = Books.Count;
            else
                limit = 4;
            Random random = new Random();
            int i = 0;
            while (i < limit)
            {
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                int index = random.Next(0, Books.Count);
                WriteLine("Title: {0}\nAuthor: {1}\nSubject: {2}", Books[index].Title, Books[index].Author, Books[index].Subject);
                ResetColor();
                WriteLine("-----------------------------");
                i++;
            }
        }
        public int Group_Dispaly(List<Book> books)
        {
            int counter = 0;
            foreach (Book temp in books)
            {
                if (temp.IsAvailable)
                    counter++;
                ForegroundColor = ConsoleColor.Black;
                BackgroundColor = ConsoleColor.White;
                Information_Of_Book(temp);
                ResetColor();
                WriteLine("============================");
            }
            return counter;
        }
        public void Information_Of_Book(Book book)
        {
            string Available_Or_Not = "this book is available";
            if (!book.IsAvailable)
                Available_Or_Not = "this book is not available";
            WriteLine("Title: {0}\nAuthor: {1}\nSubject: {2}\n{3}.", book.Title, book.Author, book.Subject, Available_Or_Not);
        }
        // this fuction displays books for a given topic
        public void Books_Subject(string subject)
        {
            List<Book> temp_Books = (from book in Books where book.Subject == subject select book).ToList();
            Group_Dispaly(temp_Books);
        }
    }
    class Menu
    {
        string[] Option;
        string Title;
        public int Selected_Index;
        public Menu(string[] Option, string Title)
        {
            this.Option = Option;
            this.Title = Title;
            Selected_Index = 0;
        }
        //only used in run method.
        private void View()
        {
            if (Selected_Index >= Option.Length)
                Selected_Index = 0;
            WriteLine(Title + "\n");
            for (int i = 0; i < Option.Length; i++)
            {
                if (i != Selected_Index)
                    WriteLine($"-{Option[i]}");
                else
                {
                    ForegroundColor = ConsoleColor.Black;
                    BackgroundColor = ConsoleColor.White;
                    WriteLine($"-{Option[i]}  <<");
                    ResetColor();
                }
            }
        }
        // this method will run the Menu.
        public int Run()
        {
            ConsoleKeyInfo KeyPressed;
            do
            {
                Clear();
                View();
                KeyPressed = ReadKey(true);
                if (KeyPressed.Key == ConsoleKey.UpArrow)
                {
                    Selected_Index--;
                    if (Selected_Index == -1)
                        Selected_Index = Option.Length - 1;
                }
                else if (KeyPressed.Key == ConsoleKey.DownArrow)
                {
                    Selected_Index++;
                    if (Selected_Index > Option.Length)
                        Selected_Index = 0;
                }
            } while (KeyPressed.Key != ConsoleKey.Enter);
            Clear();
            return Selected_Index;
        }
        // just to ask the user if he want to repeate the process or not
        static public ConsoleKeyInfo Answer_Y_N()
        {
            ConsoleKeyInfo Choois;

            while (true)
            {
                Choois = ReadKey(true);
                if (Choois.Key == ConsoleKey.Y || Choois.Key == ConsoleKey.N)
                {
                    Clear();
                    break;
                }
                if (Choois.Key != ConsoleKey.Y || Choois.Key != ConsoleKey.N)
                    continue;
            }
            return Choois;
        }
        static public (ConsoleKeyInfo, string) CaptureExitKey(string Title)
        {
            ConsoleKeyInfo KeyPressed;
            string TheString = "";
            Write(Title);
            do
            {
                KeyPressed = ReadKey(true);
                if (KeyPressed.Key == ConsoleKey.Backspace)
                {
                    if (TheString.Length > 0)
                    {
                        Clear();
                        Write(Title);
                        TheString = TheString.Remove(TheString.Length - 1);
                        Write(TheString);
                    }
                    continue;
                }
                if (KeyPressed.KeyChar != 13 && KeyPressed.KeyChar != 27)
                {
                    Write(KeyPressed.KeyChar);
                    TheString += KeyPressed.KeyChar;
                }
            } while (KeyPressed.Key != ConsoleKey.Escape && KeyPressed.Key != ConsoleKey.Enter);
            WriteLine();
            return (KeyPressed, TheString);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] Start_Menu_Option = { "Search", "Add", "Exit" };
            string[] Search_Menu_Option = { "Title & Author", "Title OR Author", "Subject", "Random books?", "Back" };
            string[] Subject_Menu_Option = { "History", "Physics", "Novels", "Philosophy", "Uncategorized", "Back" };
            Menu Search_Menu = new Menu(Search_Menu_Option, "----Search----");
            Menu Subject_Menu = new Menu(Subject_Menu_Option, "-----Subject-----");
            Menu Start_Menu = new Menu(Start_Menu_Option, @"
        ██╗     ██╗██████╗ ██████╗  █████╗ ██████╗ ██╗   ██╗
        ██║     ██║██╔══██╗██╔══██╗██╔══██╗██╔══██╗╚██╗ ██╔╝
        ██║     ██║██████╦╝██████╔╝███████║██████╔╝ ╚████╔╝
        ██║     ██║██╔══██╗██╔══██╗██╔══██║██╔══██╗  ╚██╔╝
        ███████╗██║██████╦╝██║  ██║██║  ██║██║  ██║   ██║
        ╚══════╝╚═╝╚═════╝ ╚═╝  ╚═╝╚═╝  ╚═╝╚═╝  ╚═╝   ╚═╝
=====================================================================
(Use the arrow keys to cycle through option and press enter to select.)");

            Library library = new Library();
            bool Check = true;
            while (Check)
            {
                int Start_Menu_Index = Start_Menu.Run();
                //Search
                if (Start_Menu_Index == 0)
                {
                    int Search_Menu_Index = Search_Menu.Run();
                    Clear();
                    while (true)
                    {
                        //Title & Author.
                        if (Search_Menu_Index == 0)
                        {
                            string Title, Author;
                            List<Book> temp_List;
                            ConsoleKeyInfo User_Choois;
                            try
                            {
                                (User_Choois, Title) = Menu.CaptureExitKey("ESC\n-----Search-----\nEnter the Title: ");
                                if (User_Choois.Key == ConsoleKey.Escape)
                                    break;

                                Clear();
                                (User_Choois, Author) = Menu.CaptureExitKey($"ESC\n-----Search-----\nEnter the Title: {Title}\nEnter the Authro: ");
                                if (User_Choois.Key == ConsoleKey.Escape)
                                    break;

                                temp_List = library.Search(Title, Author);
                                if (temp_List.Count == 0)
                                {
                                    Clear();
                                    WriteLine("The book not found.");
                                }
                                else
                                {
                                    Clear(); WriteLine("Matching results:");
                                    int counter = library.Group_Dispaly(temp_List);
                                    if (counter > 0)
                                    {
                                        WriteLine("Do you want to borrow one of the available books? (Y/N)");
                                        if (Menu.Answer_Y_N().Key == ConsoleKey.Y)
                                        {
                                            int i = 0;
                                            string[] Borrow_Menu_Option = new string[counter];
                                            foreach (Book temp in temp_List)
                                            {
                                                if (temp.IsAvailable)
                                                {
                                                    Borrow_Menu_Option[i] = $"{temp.Title} by {temp.Author}. /({temp.Subject}).";
                                                    i++;
                                                }
                                            }
                                            Menu Borrow_Menu = new Menu(Borrow_Menu_Option, "---Select the book you want to borrow---");

                                            int index_Borrowed_Book = Borrow_Menu.Run();
                                            foreach (Book temp in temp_List)
                                            {
                                                string trye = $"{temp.Title} by {temp.Author}. /({temp.Subject}).";
                                                if (Borrow_Menu_Option[index_Borrowed_Book].Equals(trye))
                                                {
                                                    temp.IsAvailable = false;
                                                    break;
                                                }
                                            }
                                            WriteLine("The Book will reach you soon :)");
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Clear();
                                WriteLine(ex.Message);
                                WriteLine("Enter the Title again\n");
                                continue;
                            }
                            WriteLine("Another Search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Title OR Author.
                        else if (Search_Menu_Index == 1)
                        {
                            int counter = 0;
                            List<Book> temp_List_Author;
                            List<Book> temp_List_Title;
                            ConsoleKeyInfo User_Choois;
                            string TitleOrAuthor;

                            (User_Choois, TitleOrAuthor) = Menu.CaptureExitKey("ESC.\n-----Search-----\nEnter the Title OR Author:");
                            if (User_Choois.Key == ConsoleKey.Escape)
                                break;

                            (temp_List_Author, temp_List_Title) = library.SearchByBoth(TitleOrAuthor);

                            if (temp_List_Author.Count != 0)
                            {
                                WriteLine("Books that match the Author:");
                                counter = library.Group_Dispaly(temp_List_Author);
                            }
                            else
                                WriteLine("There is no Author Match");

                            WriteLine();
                            if (temp_List_Title.Count != 0)
                            {
                                WriteLine("Books that match the Title");
                                counter += library.Group_Dispaly(temp_List_Title);
                            }
                            else
                            {
                                WriteLine("there is no book match the title");
                                if (temp_List_Author.Count == 0 && temp_List_Title.Count == 0)
                                {
                                    WriteLine("Another search? Y/N");
                                    if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                        break;
                                    else
                                        continue;
                                }
                            }
                            WriteLine("Do you want to borrow one of the available books? (Y/N)");

                            if (Menu.Answer_Y_N().Key == ConsoleKey.Y)
                            {
                                int index_Of_Borrow_Option = 0;
                                string[] Borrow_Menu_Option = new string[counter];

                                foreach (Book temp in temp_List_Title)
                                {
                                    if (temp.IsAvailable)
                                    {
                                        Borrow_Menu_Option[index_Of_Borrow_Option] = $"{temp.Title} by {temp.Author}. /({temp.Subject}).";
                                        index_Of_Borrow_Option++;
                                    }
                                }
                                foreach (Book temp in temp_List_Author)
                                {
                                    if (temp.IsAvailable)
                                    {
                                        Borrow_Menu_Option[index_Of_Borrow_Option] = $"{temp.Title} by {temp.Author}. /({temp.Subject}).";
                                        index_Of_Borrow_Option++;
                                    }
                                }
                                Menu Borrow_Menu = new Menu(Borrow_Menu_Option, "---Select the book you want to borrow---");
                                List<Book> To_Borrow_Books = temp_List_Author.Concat(temp_List_Title).ToList();
                                int index_Borrowed_Book = Borrow_Menu.Run();
                                foreach (Book temp in To_Borrow_Books)
                                {
                                    string trye = $"{temp.Title} by {temp.Author}. /({temp.Subject}).";
                                    if (Borrow_Menu_Option[index_Borrowed_Book].Equals(trye))
                                    {
                                        temp.IsAvailable = false;
                                        break;
                                    }
                                }
                                WriteLine("The Book will reach you soon :)");
                            }
                            WriteLine("Another search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Subject.
                        else if (Search_Menu_Index == 2)
                        {
                            //this line will return the index if Selected Subject
                            //and it is treated as an Arguments to the(Books_Subject)
                            //which will return a list of books related to the Selected Subject.
                            int index = Subject_Menu.Run();
                            //if Back
                            if (index == 5)
                                break;

                            library.Books_Subject(Subject_Menu_Option[index]);
                            WriteLine("Another Search? (Y/N)");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //random books .
                        else if (Search_Menu_Index == 3)
                        {
                            library.DisPlayRandomBook();
                            WriteLine("Another Search? Y/N");
                            if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                                break;
                            else
                                continue;
                        }
                        //Back
                        else if (Search_Menu_Index == 4)
                            break;
                    }
                }
                //Add
                if (Start_Menu_Index == 1)
                {
                    ConsoleKeyInfo User_Choois;
                    string Title;
                    string Author;
                    Clear();
                    while (true)
                    {
                        try
                        {
                            (User_Choois, Title) = Menu.CaptureExitKey("ESC.\n---Add a book---\nEnter the title: ");

                            if (User_Choois.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            Clear();
                            (User_Choois, Author) = Menu.CaptureExitKey($"ESC.\n---Add a book---\nEnter the title: {Title} \nEnter the author: ");
                            if (User_Choois.Key == ConsoleKey.Escape)
                            {
                                break;
                            }
                            Clear();
                            //in this line it was possible to create a variable to hold
                            //the string value from (Subject_Menu_Option[Subject_Menu.Run()]))
                            //and then but this variable as an Arguments ( The subject of the book)
                            //but I did not do that because it is a redundant variable.
                            int index = Subject_Menu.Run();
                            if (index == 5)
                                break;

                            library.Add(new Book(Title, Author, Subject_Menu_Option[index]));
                        }
                        catch (Exception ex)
                        {
                            Clear();
                            WriteLine(ex.Message);
                            continue;
                        }
                        WriteLine("add another book?\n Y/N");
                        if (Menu.Answer_Y_N().Key == ConsoleKey.N)
                            break;
                        else
                            continue;
                    }
                }
                // Exit
                if (Start_Menu_Index == 2)
                {
                    Check = false;
                    Clear();
                    WriteLine("Good_Bay!");
                }
            }
        }
    }
}
