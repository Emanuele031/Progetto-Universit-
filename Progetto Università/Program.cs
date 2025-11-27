using Models;
using Controllers;
using Repositories;
using Views;

class Program
{
    static void Main()
    {
        var controller = new UniversitaController(
            new StudenteRepository(),
            new ProfessoreRepository(),
            new CorsoRepository()
        );

        Menu.Start(controller);
    }
}

