using System.Collections.Generic;
using Models;
namespace Interfaces;

public interface ICorsoRepository
{
    void Aggiungi(CorsoDiLaurea corso);
    CorsoDiLaurea CercaPerCodice(string codice);
    List<CorsoDiLaurea> Tutti();
}
