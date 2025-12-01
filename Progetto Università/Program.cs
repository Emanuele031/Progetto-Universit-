using Models;
using Controllers;
using Repositories;
using Views;

class Program
{
    static void Main()
    {
        var controller = new UniversitaController(); // senza parametri
        Views.Menu.Start(controller);
    }
}


