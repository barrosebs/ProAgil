using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
        }
        //GERAIS
        public void Add<T>(T Entity) where T : class
        {
            _context.Add(Entity);
        }
        public void Update<T>(T Entity) where T : class
        {
            _context.Update(Entity);
        }
        public void Delete<T>(T Entity) where T : class
        {
            _context.Remove(Entity);

        }
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //EVENTOS
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                .Include(pe => pe.PalestranteEvento)
                .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(d => d.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                .Include(pe => pe.PalestranteEvento)
                .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(d => d.DataEvento)
            .Where(t => t.Tema.Contains(tema));
            
            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                .Include(pe => pe.PalestranteEvento)
                .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(d => d.DataEvento)
            .Where(t => t.EventoId == EventoId);
            
            return await query.FirstOrDefaultAsync();
        }
        //PALESTRANTES
        public Task<Palestrante> GetPalestranteAsyncById(int PalestranteId)
        {
            throw new System.NotImplementedException();
        }
                public Task<Palestrante[]> GetAllPalestrantesAsyncByNome(string nome)
        {
            throw new System.NotImplementedException();
        }


    }
}