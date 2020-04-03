using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral
         void Add<T>(T Entity) where T : class;
         void Update<T>(T Entity) where T : class;
         void Delete<T>(T Entity) where T : class;
         Task<bool> SaveChangesAsync();

         //Eventos
         Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]> GetAllEventosAsync(bool includePalestrantes);
         Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes);

         //Palestrantes
         Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome);
         Task<Palestrante> GetPalestranteAsyncById(int PalestranteId);
    }
}