using System.Collections.Generic;
using Models;
namespace Interfaces;

public interface IStudenteRepository
{
    void Aggiungi(Studente s);
    Studente CercaPerMatricola(string matricola);
    List<Studente> Tutti();
}
