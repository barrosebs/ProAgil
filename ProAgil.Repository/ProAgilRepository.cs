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
            .Where(t => t.Tema.ToLower().Contains(tema.ToLower()));
            
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
        public async Task<Palestrante> GetPalestrantesAsync(int PalestranteId, bool IncludeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedeSociais);

            if(IncludeEventos){
                query = query
                .Include(pe => pe.PalestranteEvento)
                .ThenInclude(e => e.Evento);
            }
            query = query.OrderBy(d => d.Nome)
            .Where(p => p.Id == PalestranteId);
            
            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name, bool IncludeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedeSociais);

            if(IncludeEventos){
                query = query
                .Include(pe => pe.PalestranteEvento)
                .ThenInclude(e => e.Evento);
            }
            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));
            
            return await query.ToArrayAsync();
        }



    }
}