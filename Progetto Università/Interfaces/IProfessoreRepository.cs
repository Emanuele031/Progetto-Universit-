using System.Collections.Generic;
using Models;
namespace Interfaces;

public interface IProfessoreRepository
{
    void Aggiungi(Professore professore);
    Professore CercaPerCodice(string codiceId);
    List<Professore> Tutti();
}
